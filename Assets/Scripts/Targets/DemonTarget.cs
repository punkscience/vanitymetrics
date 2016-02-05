using UnityEngine;
using System.Collections;

public class DemonTarget : MonoBehaviour {

	// demon Base Statistics
	[SerializeField]
	protected float demonBaseHealth = 3;
	[SerializeField]
	protected float demonBaseSpeed = 1;
	[SerializeField]
	protected Renderer demonRenderer;
	[SerializeField]
	protected ParticleSystem[] particles;
	[SerializeField]
	protected AudioSource scream;

	// Shit's going down.
	public delegate void EndGame( GameObject go );
	public static event EndGame OnEndGame;

	// demon statistics
	protected float demonHealth;
	protected float demonSpeed;
	protected Vector3 stunPos;
	protected float stunTimer = 0;

	// demon positioning
	protected Vector3 demonStartPoint;
	protected Vector3 demonEndPoint;

	// demon init
	protected float fadeInTimer;

	// demon death variables
	protected float deathTimer;
	protected float deathDuration = 2.5F;
	protected BoatHandler boatHandler;

	public void Init (BoatHandler handler, float healthMultiplier, float speedMultiplier, Vector3 startPosition, Vector3 endPosition) {

		// make the demon move up
		startPosition.y += 5.0F;

		// set demon variables
		boatHandler = handler;
		demonStartPoint = startPosition;
		demonEndPoint = endPosition;
		demonHealth = demonBaseHealth * healthMultiplier;
		demonSpeed = demonBaseSpeed * speedMultiplier;

		// set the demon's position
		transform.position = startPosition;
		transform.LookAt (endPosition);

		// set the transparency of the demon
		fadeInTimer = 0.0F;
		Color newColor = demonRenderer.material.color;
		newColor.a = fadeInTimer;
		demonRenderer.material.color = newColor;
	}

	void OnTriggerEnter (Collider other) {

		// Did we collide with an arrow
		if (other.gameObject.layer == 9) {

			// deal damage to the demon
			TakeDamage (other.gameObject.GetComponent<Projectile> ().damageValue);

			// destroy the arrow
			Destroy (other.gameObject);
		}

		// Did we collide with the home base?
		if (other.gameObject.layer == 12 ) {
			OnEndGame ( other.gameObject );
		}
	}

	protected void TakeDamage (float damageValue) {
		demonHealth -= damageValue;

		if (demonHealth <= 0.0F) {
			// demon is dead
			deathTimer = deathDuration;

			// disable the collider so arrows don't collide with it anymore
			gameObject.GetComponent<Collider> ().enabled = false;

			// stop the particles
			foreach (ParticleSystem ps in particles) {
				ps.Stop ();
			}

			// make it scream
			scream.pitch = 1.2F;
			scream.volume = 0.5F;
			scream.Play ();

			// tell the boat handler the demon is dead
			boatHandler.DestroyedBoat ();

		} else {

			// stun the demon
			stunTimer = 1.5F;
			stunPos = transform.position;

			// make it scream
			scream.pitch = 1.5F;
			scream.Play ();
		}
	}

	
	// Update is called once per frame
	void Update () {

		// fade in the game
		if (fadeInTimer < 1.0F) {
			fadeInTimer += Time.deltaTime * 2.0F;

			// set the transparency of the demon
			Color newColor = demonRenderer.material.color;
			newColor.a = fadeInTimer;
			demonRenderer.material.color = newColor;
		}

		// Is the demon stunned
		if (stunTimer > 0) {
			stunTimer -= Time.deltaTime;
			if (stunTimer > 0.65F) {
				transform.position = (stunPos + Random.insideUnitSphere * 0.1F);
			} else {
				if (scream.isPlaying) {
					scream.Stop ();
				}
				transform.position = stunPos;
			}

		} else if (deathTimer > 0.0F) {

			// death timer decrement
			deathTimer -= Time.deltaTime;

			// shake and lower the demon
			Vector3 deathPos = (Random.insideUnitSphere * 0.2F);
			deathPos.y = 2.0F * Time.deltaTime;
			transform.position -= deathPos;

			// set the transparency of the demon
			Color newColor = demonRenderer.material.color;
			newColor.a = Mathf.Lerp (0.0F, 1.0F, (deathTimer / deathDuration));
			demonRenderer.material.color = newColor;

			// destroy the demon once the timer is out
			if (deathTimer <= 0.0F) {
				Destroy (gameObject);
			}
		} else {
			
			// move the demon forward
			transform.position += transform.forward * (demonSpeed * Time.deltaTime);
		}
	}
}
