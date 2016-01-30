using UnityEngine;
using System.Collections;

public class DemonTarget : MonoBehaviour {

	// demon statistics
	protected float demonHealth;
	protected float demonSpeed;

	// demon positioning
	protected Vector3 demonStartPoint;
	protected Vector3 demonEndPoint;

	// demon death variables
	protected float deathTimer;

	public void Init (float health, float speed, Vector3 startPosition, Vector3 endPosition) {

		// set the demon's position
		transform.position = startPosition;
		transform.LookAt (endPosition);

		// set demon variables
		demonStartPoint = startPosition;
		demonEndPoint = endPosition;
		demonHealth = health;
		demonSpeed = speed;
	}

	void OnTriggerEnter (Collider other) {

		// Did we collide with an arrow
		if (other.gameObject.layer == 9) {

			// deal damage to the demon
			TakeDamage (other.gameObject.GetComponent<Projectile> ().damageValue);

			// destroy the arrow
			Destroy (other.gameObject);
		}
	}

	protected void TakeDamage (float damageValue) {
		demonHealth -= damageValue;

		if (demonHealth <= 0.0F) {
			deathTimer = 1.0F;

			// disable the collider so arrows don't collide with it anymore
			gameObject.GetComponent<Collider> ().enabled = false;
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (deathTimer == 0.0F) {
			transform.position += transform.forward * (demonSpeed * Time.deltaTime);
		} else {
			deathTimer -= Time.deltaTime;

			if (deathTimer <= 0.0F) {
				Destroy (gameObject);
			}
		}
	}
}
