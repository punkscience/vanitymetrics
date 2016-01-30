using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using SmartLocalization;


public class FE_Manager : MonoBehaviour {

	private LanguageManager loc;
	private GameObject MainMenu;
	private GameObject Credits;


	// Use this for initialization
	void Start () {
		MainMenu = GameObject.Find ("panel_MainMenu");
		Credits = GameObject.Find ("panel_Credits");

		loc = LanguageManager.Instance;
		LanguageManager.SetDontDestroyOnLoad ();

		ToggleMenuPanel (Credits, false);
		ToggleMenuPanel (MainMenu, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnterGame() {
		SceneManager.LoadScene (1);
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