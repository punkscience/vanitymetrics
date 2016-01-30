using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using SmartLocalization;


public class FE_Manager : MonoBehaviour {

	private LanguageManager loc;
	private GameObject MainMenu;
	private GameObject Credits;
	private AudioListener feCam;

	private AudioManager am;

	// Use this for initialization
	void Start () {



		am = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
		feCam = GameObject.Find ("FECamera").GetComponent<AudioListener>();

		MainMenu = GameObject.Find ("panel_MainMenu");
		Credits = GameObject.Find ("panel_Credits");

		loc = LanguageManager.Instance;
		LanguageManager.SetDontDestroyOnLoad ();

		ToggleMenuPanel (Credits, false);
		ToggleMenuPanel (MainMenu, true);

		am.StartMainMenuMusic ();
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Main Menu is : " + MainMenu.activeSelf);
	}

	public void EnterGame() {
		am.TransitionToAmbient ();
		feCam.enabled = false;
		ToggleMenuPanel (MainMenu, false);
		ToggleMenuPanel (Credits, false);
		SceneManager.LoadScene ("Main", LoadSceneMode.Additive);
	}

	public void ChangeLanguage(string lang) {
		loc.ChangeLanguage(lang);
	}

	public void ShowCredits() {
		ToggleMenuPanel (MainMenu, false);
		ToggleMenuPanel (Credits, true);
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