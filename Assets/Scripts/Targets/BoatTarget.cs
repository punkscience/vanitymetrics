using UnityEngine;
using System.Collections;

public class BoatTarget : MonoBehaviour {

	protected float boatSpeed = 0;
	protected float boatDeathTimer = 0;

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
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.layer == 8) {
			SinkShip ();
		}
	}

	void SpawnDemon () {
		
	}

	void SinkShip () {
		// stop the boat death timer so that a demon doesn't spawn
		boatDeathTimer = 0;
	}
}
