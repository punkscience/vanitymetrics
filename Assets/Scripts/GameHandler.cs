using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

	[SerializeField]
	protected BoatHandler boatHandlerPrefab;
	[SerializeField]
	protected Transform boatParent;
	[SerializeField]
	protected DemonHandler demonHandler;

	// Waves
	protected int waveCount = 0;
	protected BoatHandler currentWave;
	protected float waveIntervalTimer = 0;
	protected bool waveIntervalStart;

	// Instance
	public static GameHandler Instance;

	// Use this for initialization
	void Start () {
		
		if (Instance != null) {
			Destroy (gameObject);
		}
		Instance = this;

		Reset ();
	}
	
	// Update is called once per frame
	void Update () {

		if (waveIntervalStart) {
			waveIntervalTimer -= Time.deltaTime;

			if (waveIntervalTimer <= 0.0F) {
				StartWave ();
			}
		}
	}

	public void StartWave () {

		waveCount++;

		// remove the current wave
		if (currentWave != null) {
			currentWave.isCurrentWave = false;
		}

		// start a new boat wave
		currentWave = null;
		currentWave = Instantiate (boatHandlerPrefab);
		currentWave.transform.SetParent (transform);
		currentWave.StartWave (boatParent, 5, 2, new Vector2 (20.0F, 20.0F));
		currentWave.isCurrentWave = true;
		demonHandler.StartWave (new Vector2 (1.0F, 4.0F), new Vector2 (0.5F, 1.0F));

		// set the wave interval
		waveIntervalTimer = 20.0F;
		waveIntervalStart = false;
	}

	public void FinishedSendingWave () {
		
		waveIntervalStart = true;
	}

	public void SpawnDemon (Transform startPosition) {

		demonHandler.SpawnDemon (startPosition);
	}

	public void Reset () {

		waveCount = 0;

		foreach (Transform child in boatParent) {
			Destroy (child.gameObject);
		}
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

		StartWave ();
	}
}
