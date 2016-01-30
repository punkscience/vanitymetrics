using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatHandler : MonoBehaviour {

	[SerializeField]
	protected BoatTarget boatPrefab;
	protected Transform boatParent;

	// wave variables
	public bool isCurrentWave;
	protected int boatsToRelease = 0;
	protected Vector2 boatReleaseTimerRange;
	protected float boatReleaseTimer;
	protected float boatSpeed = 0;
	protected float lastAngle = 0;
	protected int currentBoatIndex = 0;
	protected Vector2 boatDeathRange;
	protected List<BoatTarget> boats;
	protected int destroyedBoatCount;

	void Update () {

		if (boatsToRelease > 0) {

			// decrement boat release timer
			boatReleaseTimer -= Time.deltaTime;
			if (boatReleaseTimer <= 0.0F) {

				// choose an angle
				float angle = 0.0F;
				do {
					angle = -10.0F * Random.Range (-2, 3);
				} while (angle == lastAngle);
				lastAngle = angle;

				// death range
				float deathTime = Random.Range (boatDeathRange.x, boatDeathRange.y);

				// create a boat
				BoatTarget newBoat = (BoatTarget) Instantiate (boatPrefab);
				newBoat.transform.SetParent (boatParent);
				newBoat.Init (currentBoatIndex, angle, boatSpeed, deathTime);
				boats.Add (newBoat);
				currentBoatIndex++;

				// 
				boatReleaseTimer = Random.Range (boatReleaseTimerRange.x, boatReleaseTimerRange.y);
			}
		}
	}
	
	public void StartWave (Transform boatStartLoc, int boatCount, float speed, Vector2 deathRange) {

		boatParent = boatStartLoc;
		boatsToRelease = boatCount;
		boatSpeed = speed;
		float intervalSpeed = 1 / speed;
		boatReleaseTimerRange = new Vector2 (intervalSpeed + 2.5F, (intervalSpeed * 10.5F) + 2.5F);
		boatDeathRange = deathRange;
		currentBoatIndex = 0;
		boatReleaseTimer = 0;

		boats = new List<BoatTarget> ();
	}

	public void BoatDestroyed (BoatTarget boat) {
		
		// increment the destroyed boat counter
		destroyedBoatCount++;

		if (boatsToRelease <= 0 && destroyedBoatCount >= boats.Count) {

			// if it's the most recent wave, tell GameHandler to start a new wave
			if (isCurrentWave) {
				GameHandler.Instance.StartWave ();
			}

			Destroy (gameObject);

		}
	}
}
