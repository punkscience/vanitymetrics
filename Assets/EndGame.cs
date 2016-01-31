using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject bloodRed;
	public GameObject demonFace;
	private GameObject demon = null;

	private bool holdDemon = false;

	// Use this for initialization
	void Start () {
	
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
		
		// Blood red screens!!
		bloodRed.SetActive ( true );

		var tween = Tween

		// Make my own imobile demon.
		demon = Instantiate (go);
		holdDemon = true;
	}


	void OnDisable() {
		DemonTarget.OnEndGame -= DoEndGame;
	}


	public void ExitGame() {
		SceneManager.LoadScene( "FrontEnd", LoadSceneMode.Single );
	}

}
