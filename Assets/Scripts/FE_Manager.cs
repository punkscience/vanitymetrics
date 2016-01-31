using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FE_Manager : MonoBehaviour {

	public static bool isPaused = false;
	private GameObject MainMenu;
	private GameObject Credits;
	private GameObject Pause;
	private GameObject HowToPlay;
	private AudioListener feCam;
	private GameObject feFire;

	private AudioManager am;

	// Use this for initialization
	void Start () {

		am = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
		feCam = GameObject.Find ("FECamera").GetComponent<AudioListener>();
		feFire = GameObject.Find ("FE_Fire");

		MainMenu = GameObject.Find ("panel_MainMenu");
		Credits = GameObject.Find ("panel_Credits");
		HowToPlay = GameObject.Find ("panel_HowToPlay");

		ToggleMenuPanel (MainMenu, true);
		ToggleMenuPanel (Credits, false);

		am.StartMainMenuMusic ();

		Invoke ("TurnOnFire", 1f);
	
	}
	
	void TurnOnFire () {
		feFire.GetComponent<ParticleSystem> ().Play ();
	}
	// Update is called once per frame
	void Update () {
	}

	public void TogglePause() {
		Debug.Log ("FE Manager isPaused: " + isPaused);
		if (!isPaused) { 
			isPaused = true;
			Time.timeScale = 0f;

		} else {
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
		ToggleMenuPanel (HowToPlay, false);
	}

	void ToggleMenuPanel (GameObject panel, bool active) {
			panel.SetActive (active);
	}

	public void ShowHowToPlay() {
		ToggleMenuPanel (MainMenu, false);
		ToggleMenuPanel (HowToPlay, true);
	}
}