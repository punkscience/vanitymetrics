using UnityEngine;
using System.Collections;

public class BoatTarget : MonoBehaviour {

	protected float boatSpeed = 0;
	protected float boatDeathTimer = 0;
	protected float boatSinkTimer = 0;
	protected float boatSinkDuration = 10.0F;
	protected bool fireStarted = false;
	protected float fireStopTime = 5.0F;
	protected float boatInitSinkSpeed = 0;
	[SerializeField]
	protected float boatHeight = 0;
	[SerializeField]
	protected ParticleSystem fireSystem;

	public void Init (float angle, float speed) {
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = new Vector3 (0, angle, 0);
		boatSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += transform.forward * (boatSpeed * Time.deltaTime);

		// decrement the boat death timer
		if (boatDeathTimer > 0) {
			boatDeathTimer -= Time.deltaTime;

			if (boatDeathTimer <= 0) {
				SpawnDemon ();
			}
		}

		// decrement the sink timer
		if (boatSinkTimer > 0) {
			boatSpeed -= boatInitSinkSpeed * Time.deltaTime / boatSinkDuration;
			Vector3 height = transform.localPosition;
			height.y -= boatHeight * Time.deltaTime / boatSinkDuration;
			transform.localPosition = height;

			boatSinkTimer -= Time.deltaTime;

			if (fireStarted && boatSinkTimer <= fireStopTime) {
				fireStarted = false;
				fireSystem.Stop ();
			}

			if (boatSinkTimer <= 0) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == 9) {
			SinkShip ();
		}
	}

	void SpawnDemon () {
		
	}

	void SinkShip () {
		// stop the boat death timer so that a demon doesn't spawn
		boatDeathTimer = 0;
		boatInitSinkSpeed = boatSpeed;
		boatSinkTimer = boatSinkDuration;

		fireStarted = true;
		fireSystem.Play ();
	}
}
