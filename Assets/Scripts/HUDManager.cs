using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour {

	public static bool isPaused = false;

	private GameObject pauseMenu;
	private GameObject HUD;
	private GameObject youWin;

	// Use this for initialization
	void Start () {
		
		pauseMenu = GameObject.Find ("panel_PauseMenu");
		HUD = GameObject.Find ("panel_HUD");
		youWin = GameObject.Find ("panel_YouWin");


		if (pauseMenu.activeSelf) { pauseMenu.SetActive (false); }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPause(bool isPaused) {
		if (isPaused) {
			Time.timeScale = 0f;
			pauseMenu.SetActive (true);
			HUD.SetActive (false);

		} else {
			Time.timeScale = 1f;
			pauseMenu.SetActive (false);
			HUD.SetActive (true);
		}
	}

	public void ReturnToMain() {
		SceneManager.LoadScene ("FrontEnd");
	}

	public void ReloadGame() {
		SceneManager.LoadScene("Main");
	}
}