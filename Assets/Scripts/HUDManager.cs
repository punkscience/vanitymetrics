using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour {

	public static bool isPaused = false;
	public static bool invertXAxis;
	public static bool invertYAxis;

    [SerializeField] private GameObject _pauseCG;
    [SerializeField] private GameObject _settingsCG;

    [SerializeField] private Toggle _xToggle;
    [SerializeField] private ParticleSystem _xToggleFire;
    [SerializeField] private Toggle _yToggle;
    [SerializeField] private ParticleSystem _yToggleFire;

	private GameObject pauseMenu;
	private GameObject HUD;
	private GameObject youWin;

	void Start () {
		pauseMenu = GameObject.Find ("panel_PauseMenu");
		HUD = GameObject.Find ("panel_HUD");
		if (pauseMenu.activeSelf) { pauseMenu.SetActive (false); }

	    invertXAxis = SettingsInvertX;
	    invertYAxis = SettingsInvertY;
	    _xToggle.isOn = SettingsInvertX;
	    _yToggle.isOn = SettingsInvertY;

        if (_xToggle.isOn) _xToggleFire.Play(true);
        else _xToggleFire.Play(true);

        if (_yToggle.isOn) _yToggleFire.Play(true);
        else _yToggleFire.Play(true);


	}

	public void InverseXAxis () {
		invertXAxis = !invertXAxis;
	}

    public void InvertXAxis(bool val) {
        SettingsInvertX = val;
        invertXAxis = val;
        if (val)
            _xToggleFire.Play(true);
        else 
            _xToggleFire.Stop(true);
    }

    public void InvertYAxis(bool val) {
        SettingsInvertY = val;
        invertYAxis = val;
        if (val)
            _yToggleFire.Play(true);
        else 
            _yToggleFire.Stop(true);
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

    public void ToSettingsMenu() {
        TweenCGAlpha.Tween(_pauseCG, 0.3f, 1f, 0f, () => { _pauseCG.SetActive(false); });
        _settingsCG.SetActive(true);
        TweenCGAlpha.Tween(_settingsCG, 0.3f, 0f, 1f);
    }

    public void ToPauseMenu() {
        TweenCGAlpha.Tween(_settingsCG, 0.3f, 1f, 0f, () => { _settingsCG.SetActive(false); });
        _pauseCG.SetActive(true);
        TweenCGAlpha.Tween(_pauseCG, 0.3f, 0f, 1f);
        
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

    private bool SettingsInvertX {
        get { return PlayerPrefs.GetInt("SettingsInvertX", 0) == 1; }
        set { PlayerPrefs.SetInt("SettingsInvertX", value ? 1 : 0); }
    }

    private bool SettingsInvertY {
        get { return PlayerPrefs.GetInt("SettingsInvertY", 0) == 1; }
        set { PlayerPrefs.SetInt("SettingsInvertY", value ? 1 : 0); }
    }
}
