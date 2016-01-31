using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject bloodRed;
	public GameObject demonFace = null;
	private GameObject demon = null;

	private bool holdDemon = false;
	private GameObject HUD;

	private GameObject GameOver;
	private GameObject TryAgain;
	private GameObject AdmitDefeat;

	// Use this for initialization
	void Start () {
		HUD = GameObject.Find ("panel_HUD");

		GameOver = GameObject.Find ("txt_YouLose"); 
		TryAgain = GameObject.Find ("btn_TryAgain");
		AdmitDefeat = GameObject.Find ("btn_AdmitDefeat");
			
	}
	
	// Update is called once per frame
	void Update () {
		if (holdDemon == true ) {
			Debug.Log ("Holding demon!!");
			Vector3 demonPos = demonFace.transform.position;
			demon.transform.position = demonPos;
		}
	}


	void OnEnable() {
		DemonTarget.OnEndGame += DoEndGame;
	}


	void DoEndGame( GameObject go ) {
		// rm HUD
		if (HUD.activeSelf) {	HUD.SetActive (false); }
		HUDManager.isPaused = true;
		// Blood red screens!!
		bloodRed.SetActive ( true );
		AudioSource screams = GetComponent<AudioSource> ();
		screams.Play ();
		Invoke ("FillInScreenContent", 2f);

		// Make my own imobile demon.
		demon = Instantiate (go);
		holdDemon = true;
	}


	void OnDisable() {
		DemonTarget.OnEndGame -= DoEndGame;
	}

	public void FillInScreenContent() {
		GameOver.SetActive (true);
	    TryAgain.SetActive (true);
		AdmitDefeat.SetActive (true);
	}

	public void ExitGame() {
		SceneManager.LoadScene( "FrontEnd" );
	}

}
