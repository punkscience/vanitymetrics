using UnityEngine;
using System.Collections;

public class DemonHandler : MonoBehaviour {

	[SerializeField]
	protected DemonTarget[] demonPrefabs;
	[SerializeField]
	protected Transform demonTarget;
	[SerializeField]
	protected Transform demonParent;

	// demon statistics
	protected float demonHealthMultiplier;
	protected float demonSpeedMultiplier;


	public void StartWave (float health, float speed) {
		
		demonHealthMultiplier = health;
		demonSpeedMultiplier = speed;
	}

	public void SpawnDemon (Transform boatStartPoint, BoatHandler handler) {

		// choose a demon
		int demonIndex = 0;
//		int demonIndex = Random.Range (0, demonPrefabs.Length);

		// create a demon
		DemonTarget newDemon = Instantiate (demonPrefabs [demonIndex]);
		newDemon.transform.SetParent (demonParent);

		// initialize the demon
		newDemon.Init (
			handler, 
			demonHealthMultiplier,
			demonSpeedMultiplier,
			boatStartPoint.position, 
			demonTarget.position);
	}
}
