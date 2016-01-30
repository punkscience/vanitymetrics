using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	[SerializeField]
	protected BoatHandler boatHandler;
	[SerializeField]
	protected DemonHandler demonHandler;

	public static GameHandler Instance;

	// Use this for initialization
	void Start () {
		if (Instance != null) {
			Destroy (gameObject);
		}
		Instance = this;

		boatHandler.StartWave (50, 2, new Vector2 (20.0F, 20.0F));
		demonHandler.StartWave (new Vector2 (2.0F, 4.0F), new Vector2 (1.5F, 3.5F));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnDemon (Transform startPosition) {

		demonHandler.SpawnDemon (startPosition);
	}
}
