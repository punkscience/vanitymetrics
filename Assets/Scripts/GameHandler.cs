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

	public delegate void EndGame();
	public static event EndGame OnWinGame;

	// Waves
	protected int waveCount = 0;
	protected BoatHandler currentWave;
	protected float waveIntervalTimer = 0;
	protected bool waveIntervalStart;

	// Wave Data
	protected List<List<BoatWaveData>> waveData = new List<List<BoatWaveData>> () {
		new List<BoatWaveData> () {
			new BoatWaveData (0, 2, 0)
		}, 
		new List<BoatWaveData> () {
			new BoatWaveData (0, 1, 0),
			new BoatWaveData (0, 3, 2)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (0, 4, 0),
			new BoatWaveData (1, 1, 2)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (0, 0, 0),
			new BoatWaveData (1, 3, 2)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (0, 1, 0),
			new BoatWaveData (0, 3, 0),
			new BoatWaveData (1, 2, 3)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (1, 0, 0),
			new BoatWaveData (1, 4, 0),
			new BoatWaveData (0, 2, 4),
			new BoatWaveData (1, 1, 4),
			new BoatWaveData (1, 3, 0)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (0, 4, 0),
			new BoatWaveData (0, 3, 1),
			new BoatWaveData (0, 2, 1),
			new BoatWaveData (0, 1, 1),
			new BoatWaveData (0, 0, 1)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (2, 2, 0)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (0, 1, 0),
			new BoatWaveData (0, 3, 0),
			new BoatWaveData (1, 2, 3),
			new BoatWaveData (1, 0, 0),
			new BoatWaveData (1, 4, 0)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (0, 0, 0),
			new BoatWaveData (1, 4, 0),
			new BoatWaveData (2, 2, 0)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (1, 0, 0),
			new BoatWaveData (1, 1, 1),
			new BoatWaveData (1, 2, 1),
			new BoatWaveData (1, 3, 1),
			new BoatWaveData (1, 4, 1)
		},
		new List <BoatWaveData> () {
			new BoatWaveData (2, 0, 0),
			new BoatWaveData (2, 4, 0),
			new BoatWaveData (1, 1, 0),
			new BoatWaveData (1, 3, 0),
			new BoatWaveData (0, 2, 0)
		}
	};

	// wave effects
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

		// remove the current wave
		if (currentWave != null) {
			currentWave.isCurrentWave = false;
		}

		// start a new boat wave
		currentWave = null;
		currentWave = Instantiate (boatHandlerPrefab);
		currentWave.transform.SetParent (transform);
		currentWave.isCurrentWave = true;

		// Start a new Wave
		if (waveCount < waveData.Count) {
			currentWave.StartWave (boatParent, waveData [waveCount]);
		}

		// demon waves
		demonHandler.StartWave (demonHealthMod, demonSpeedMod);

//		// increment wave stats
		demonSpeedMod += 0.125F;

//		// set the wave interval
		waveIntervalTimer = 2.0F;
		waveIntervalStart = false;

		// increment the wave
		waveCount++;
	}

	public void FinishedWave () {

		// Check if the game is finished
		if (waveCount >= waveData.Count) {
			OnWinGame ();
			return;
		}

		waveIntervalStart = true;
	}

	public void SpawnDemon (Transform startPosition, BoatHandler handler) {

		demonHandler.SpawnDemon (startPosition, handler);
	}

	public void Reset () {

		// wave stats
		waveCount = 0;
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
