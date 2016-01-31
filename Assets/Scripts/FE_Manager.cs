using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FE_Manager : MonoBehaviour {

	public static bool isPaused = false;
	private GameObject MainMenu;
	private GameObject Credits;
	private GameObject Pause;
	private GameObject HUD;
	private AudioListener feCam;

	private AudioManager am;

	// Use this for initialization
	void Start () {

		am = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
		feCam = GameObject.Find ("FECamera").GetComponent<AudioListener>();

		MainMenu = GameObject.Find ("panel_MainMenu");
		Credits = GameObject.Find ("panel_Credits");
		Pause = GameObject.Find ("panel_PauseMenu");
		HUD = GameObject.Find ("panel_HUD"); 

		ToggleMenuPanel (MainMenu, true);
		ToggleMenuPanel (Credits, false);
		ToggleMenuPanel (Pause, false);
		ToggleMenuPanel (HUD, false);

		am.StartMainMenuMusic ();

		Invoke ("TurnOnFire", 1f);
	
	}
	
	void TurnOnFire () {
		GameObject.Find ("FE_Fire").GetComponent<ParticleSystem> ().Play ();
	}
	// Update is called once per frame
	void Update () {
	}

	public void TogglePause() {
		Debug.Log ("FE Manager isPaused: " + isPaused);
		if (!isPaused) { 
			ToggleMenuPanel (Pause, true);
			ToggleMenuPanel (HUD, false);
			isPaused = true;
			Time.timeScale = 0f;

		} else {
			ToggleMenuPanel (Pause, false);
			ToggleMenuPanel (HUD, true);
			isPaused = false;
			Time.timeScale = 1f;
		}
	}
	public void EnterGame() {
		am.TransitionToAmbient ();
		feCam.enabled = false;
		ToggleMenuPanel (MainMenu, false);
		ToggleMenuPanel (HUD, true);
		SceneManager.LoadScene ("Main", LoadSceneMode.Additive);
		isPaused = false;
	}
		
	public void ShowCredits() {
		ToggleMenuPanel (MainMenu, false);
		ToggleMenuPanel (Credits, true);
	}


	public void ShowOptions() {
		ToggleMenuPanel (MainMenu, false);
		ToggleMenuPanel (Credits, false);
	}

	public void ShowMainMenu() {
		ToggleMenuPanel (MainMenu, true);
		ToggleMenuPanel (Credits, false);
	}

	void ToggleMenuPanel (GameObject panel, bool active) {
			panel.SetActive (active);
	}
}