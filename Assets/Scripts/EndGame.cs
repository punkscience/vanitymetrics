using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject bloodRed;
//	public GameObject demonFace = null;
//	private GameObject demon = null;

	private GameObject HUD;
	private GameObject panelYouWin;



	// Use this for initialization
	void Start () {
		HUD = GameObject.Find ("panel_HUD");

	
	}
	
	// Update is called once per frame
	void Update () {
//		if (holdDemon == true ) {
//			Debug.Log ("Holding demon!!");
//			Vector3 demonPos = demonFace.transform.position;
//			demon.transform.position = demonPos;
//		}
	}


	void OnEnable() {
		DemonTarget.OnEndGame += DoEndGame;
		GameHandler.OnWinGame += WinGame;
	}


	void DoEndGame( GameObject go ) {
		// rm HUD
		if (HUD.activeSelf) {	HUD.SetActive (false); }
		HUDManager.isPaused = true;
		// Blood red screens!!
		bloodRed.SetActive ( true );
		AudioSource screams = GetComponent<AudioSource> ();
		screams.Play ();

		// Make my own imobile demon.
//		demon = Instantiate (go);
//		holdDemon = true;
	}


	void OnDisable() {
		DemonTarget.OnEndGame -= DoEndGame;
		GameHandler.OnWinGame -= WinGame;
	}
		
	public void ExitGame() {
		SceneManager.LoadScene( "FrontEnd" );
	}


	void WinGame () {
		// rm HUD
		if (HUD.activeSelf) {	HUD.SetActive (false); }

		HUDManager.isPaused = true;

		// Blood red screens!!
		panelYouWin.SetActive ( true );

	}
		

}
