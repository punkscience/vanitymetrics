using UnityEngine;
using System.Collections;

public class Enabler : MonoBehaviour {

	public GameObject panel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		panel.SetActive(true);
	}

	public void TurnOff() {
		panel.SetActive (false);
	}
}
