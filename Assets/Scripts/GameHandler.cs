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

	// wave effects
	protected float boatsCount;
	protected float boatSpeedMod;
	protected float demonSpeedMod;
	protected float demonHealthMod;

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

		Debug.Log ("Starting Wave " + waveCount);

		waveCount++;

		// remove the current wave
		if (currentWave != null) {
			currentWave.isCurrentWave = false;
		}

		// start a new boat wave
		currentWave = null;
		currentWave = Instantiate (boatHandlerPrefab);
		currentWave.transform.SetParent (transform);
		currentWave.isCurrentWave = true;

		// wave mods
		float boatCountPercentage = Random.Range (0, 1);
		float boatSpeedPercentage = 1.0F - boatCountPercentage;
		currentWave.StartWave (
			boatParent, 
			((int)Mathf.Floor (boatsCount * (boatCountPercentage + 0.5F))), 
			(boatSpeedMod * (boatSpeedPercentage + 0.5F)));

		// demon waves
		demonHandler.StartWave (demonHealthMod, demonSpeedMod);

		// increment wave stats
		boatsCount += 1.5F;
		boatSpeedMod += 0.25F;
		demonSpeedMod += 0.125F;

		// set the wave interval
		waveIntervalTimer = 60.0F;
		waveIntervalStart = false;
	}

	public void FinishedSendingWave () {
		
		waveIntervalStart = true;
	}

	public void SpawnDemon (Transform startPosition) {

		demonHandler.SpawnDemon (startPosition);
	}

	public void Reset () {

		// wave stats
		waveCount = 0;
		boatsCount = 5.0F;
		boatSpeedMod = 2.0F;
		demonSpeedMod = 1.0F;
		demonHealthMod = 1.0F;

		// destroy all current wave objects
		foreach (Transform child in boatParent) {
			Destroy (child.gameObject);
		}
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

		// start the wave
		StartWave ();
	}
}
