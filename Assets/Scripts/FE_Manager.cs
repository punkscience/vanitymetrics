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

		if (isPaused) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}

	public void TogglePause() {
		Debug.Log ("paused");
		if (isPaused) { 
			ToggleMenuPanel (Pause, true);
			ToggleMenuPanel (HUD, false);
			isPaused = false;

		} else {
			ToggleMenuPanel (Pause, false);
			ToggleMenuPanel (HUD, true);
			isPaused = true;
		}
	}
	public void EnterGame() {
		am.TransitionToAmbient ();
		feCam.enabled = false;
		ToggleMenuPanel (MainMenu, false);
		ToggleMenuPanel (HUD, true);
		SceneManager.LoadScene ("Main", LoadSceneMode.Additive);
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
		if (active) {
			panel.SetActive (true);
		} else {
			panel.SetActive (false);
		}
	}
}