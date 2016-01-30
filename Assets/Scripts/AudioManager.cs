using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	[SerializeField]
	private AudioSource feMusic;

	[SerializeField]
	private AudioSource ambience;

	public void PlayMainMenuMusic() {
		feMusic.Play();
	}


	public void StopMainMenuMusic() { 
		feMusic.Stop();
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
