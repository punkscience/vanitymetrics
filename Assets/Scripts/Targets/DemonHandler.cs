using UnityEngine;
using System.Collections;

public class DemonHandler : MonoBehaviour {

	[SerializeField]
	protected DemonTarget[] demonPrefabs;
	[SerializeField]
	protected Transform demonTarget;

	// demon statistics
	protected Vector2 demonHealthMultiplierRange;
	protected Vector2 demonSpeedMultiplierRange;


	public void StartWave (Vector2 health, Vector2 speed) {
		
		demonHealthMultiplierRange = health;
		demonSpeedMultiplierRange = speed;
	}

	public void SpawnDemon (Transform boatStartPoint) {

		// choose a demon
		int demonIndex = Random.Range (0, demonPrefabs.Length);

		// create a demon
		DemonTarget newDemon = Instantiate (demonPrefabs [demonIndex]);

		// initialize the demon
		newDemon.Init (
			Random.Range (demonHealthMultiplierRange.x, demonHealthMultiplierRange.y),
			Random.Range (demonSpeedMultiplierRange.x, demonSpeedMultiplierRange.y),
			boatStartPoint.position, 
			demonTarget.position);
	}
}
