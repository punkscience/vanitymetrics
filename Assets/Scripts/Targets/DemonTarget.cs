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

	// demon death variables
	protected float deathTimer;
	protected BoatHandler boatHandler;

	public void Init (BoatHandler handler, float healthMultiplier, float speedMultiplier, Vector3 startPosition, Vector3 endPosition) {

		// set the demon's position
		transform.position = startPosition;
		transform.LookAt (endPosition);

		// set demon variables
		boatHandler = handler;
		demonStartPoint = startPosition;
		demonEndPoint = endPosition;
		demonHealth = demonBaseHealth * healthMultiplier;
		demonSpeed = demonBaseSpeed * speedMultiplier;
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
			deathTimer = 1.0F;

			// disable the collider so arrows don't collide with it anymore
			gameObject.GetComponent<Collider> ().enabled = false;

			// tell the boat handler the demon is dead
			boatHandler.DestroyedBoat ();

		} else {

			// stun the demon
			stunTimer = 1.5F;
			stunPos = transform.position;
		}
	}

	
	// Update is called once per frame
	void Update () {

		if (stunTimer > 0) {
			stunTimer -= Time.deltaTime;
			if (stunTimer > 1.0F) {
				transform.position = (stunPos + Random.insideUnitSphere * 0.1F);
			} else {
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
			Color newColor = Color.black;
			newColor.a = Mathf.Lerp (0.0F, 1.0F, deathTimer);
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
