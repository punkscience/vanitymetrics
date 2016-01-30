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
	protected float lastAngle = 0;
	protected Vector2 boatDeathRange;

	// Use this for initialization
	void Start () {
		StartWave (50, 2, new Vector2 (20.0F, 20.0F));
	}

	void Update () {

		if (boatsToRelease > 0.0F) {

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
				newBoat.Init (angle, boatSpeed, deathTime);

				boatReleaseTimer = Random.Range (boatReleaseTimerRange.x, boatReleaseTimerRange.y);
			}
		}
	}
	
	public void StartWave (int boatCount, float speed, Vector2 deathRange) {

		boatsToRelease = boatCount;
		boatSpeed = speed;
		float intervalSpeed = 1 / speed;
		boatReleaseTimerRange = new Vector2 (intervalSpeed + 2.5F, (intervalSpeed * 10.5F) + 2.5F);
		boatDeathRange = deathRange;

		boatReleaseTimer = 0;
	}
}
