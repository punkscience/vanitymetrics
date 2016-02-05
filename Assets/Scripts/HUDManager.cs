using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour {

	public static bool isPaused = false;
	public static bool invertXAxis = false;

	private GameObject pauseMenu;
	private GameObject HUD;
	private GameObject youWin;

	// Use this for initialization
	void Start () {
		
		pauseMenu = GameObject.Find ("panel_PauseMenu");
		HUD = GameObject.Find ("panel_HUD");

		if (pauseMenu.activeSelf) { pauseMenu.SetActive (false); }
	}

	public void InverseXAxis () {
		invertXAxis = !invertXAxis;
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
		isPaused = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene ("FrontEnd");
	}

	public void LoseGame() {
		isPaused = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Lose");
	}

	public void ReloadGame() {
		isPaused = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene("Main");
	}
}