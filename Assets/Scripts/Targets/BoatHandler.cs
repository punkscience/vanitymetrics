using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatHandler : MonoBehaviour {

	[SerializeField]
	protected BoatTarget boatPrefab;
	protected Transform boatParent;

	// wave variables
	public bool isCurrentWave;
	protected List<BoatWaveData> waveData;
	protected int boatsToRelease = 0;
	protected int releasedBoatsCounter = 0;
	protected Vector2 boatReleaseTimerRange;
	protected float boatReleaseTimer;
//	protected float boatSpeed = 0;
//	protected float lastAngle = 0;
	protected int currentBoatIndex = 0;
	protected int destroyedBoatCount;

	void Update () {

		if (releasedBoatsCounter < boatsToRelease) {

			// decrement boat release timer
			boatReleaseTimer -= Time.deltaTime;
			if (boatReleaseTimer <= 0.0F) {

				// choose an angle
//				float angle = 0.0F;
//				do {
//					angle = -10.0F * Random.Range (-2, 3);
//				} while (angle == lastAngle);
//				lastAngle = angle;

				// create a boat
				BoatTarget newBoat = (BoatTarget) Instantiate (boatPrefab);
				newBoat.transform.SetParent (boatParent);
				newBoat.Init (this, 
					waveData[currentBoatIndex].angle, 
					waveData[currentBoatIndex].speed, 
					waveData[currentBoatIndex].boatColorIndex);

				// increment intervals
				currentBoatIndex++;
				releasedBoatsCounter++;

				// release timer
				if (currentBoatIndex < waveData.Count) {
					boatReleaseTimer = waveData [currentBoatIndex].releaseInterval;
				}

//				boatReleaseTimer = Random.Range (boatReleaseTimerRange.x, boatReleaseTimerRange.y);

				// increment the released boats counter

//				if (isCurrentWave && releasedBoatsCounter >= boatsToRelease) {
//					GameHandler.Instance.FinishedSendingWave ();
//				}

			}
		}
	}
	
	public void StartWave (Transform boatStartLoc, List<BoatWaveData> boatWaveData) {

		boatParent = boatStartLoc;
		waveData = boatWaveData;
		boatsToRelease = boatWaveData.Count;
//		boatSpeed = speed;
//		float intervalSpeed = 1 / speed;
//		boatReleaseTimerRange = new Vector2 (intervalSpeed + 2.5F, (intervalSpeed * 10.5F) + 2.5F);
		boatReleaseTimer = 0;
		currentBoatIndex = 0;
		releasedBoatsCounter = 0;
		destroyedBoatCount = 0;
	}

	public void DestroyedBoat () {
		
		// increment the destroyed boat counter
		destroyedBoatCount++;

		if (destroyedBoatCount >= boatsToRelease) {

			// if it's the most recent wave, tell GameHandler to start a new wave
			if (isCurrentWave) {
				GameHandler.Instance.FinishedWave ();
				isCurrentWave = false;
			}

			Destroy (gameObject);
		}
	}
}
