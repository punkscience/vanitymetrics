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

		ToggleMenuPanel (MainMenu, true);
		ToggleMenuPanel (Credits, false);
		ToggleMenuPanel (Pause, false);

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
			isPaused = true;
			Time.timeScale = 0f;

		} else {
			ToggleMenuPanel (Pause, false);
			isPaused = false;
			Time.timeScale = 1f;
		}
	}
	public void EnterGame() {
		am.TransitionToAmbient ();
		feCam.enabled = false;
		ToggleMenuPanel (MainMenu, false);
		SceneManager.LoadScene ("Main");
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