using UnityEngine;
using System.Collections;

public class BoatHandler : MonoBehaviour {

	[SerializeField]
	protected BoatTarget boatPrefab;
	[SerializeField]
	protected Transform boatParent;

	// wave variables
	protected int boatsToRelease = 0;
	protected Vector2 boatReleaseTimerRange;
	protected float boatReleaseTimer;
	protected float boatSpeed = 0;

	// Use this for initialization
	void Start () {
		StartWave (50, 2, new Vector2 (2, 8));
	}

	void Update () {

		if (boatsToRelease > 0) {

			// decrement boat release timer
			boatReleaseTimer -= Time.deltaTime;
			if (boatReleaseTimer <= 0.0F) {

				// choose an angle
				float angle = -10F * Random.Range (-2, 2);

				// create a boat
				BoatTarget newBoat = (BoatTarget) Instantiate (boatPrefab);
				newBoat.transform.SetParent (boatParent);
				newBoat.Init (angle, boatSpeed);

				boatReleaseTimer = Random.Range (boatReleaseTimerRange.x, boatReleaseTimerRange.y);
			}
		}
	}
	
	public void StartWave (int boatCount, float speed, Vector2 boatReleaseIntervalRange) {

		boatsToRelease = boatCount;
		boatSpeed = speed;
		boatReleaseTimerRange = boatReleaseIntervalRange;

		boatReleaseTimer = 0;
	}
}
