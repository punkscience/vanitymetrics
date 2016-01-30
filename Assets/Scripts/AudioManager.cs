using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {


	public int fadeTime = 3;

	[SerializeField]
	private AudioSource feMusic;

	[SerializeField]
	private AudioSource ambience;

	public void PlayMainMenuMusic() {
		FadeMusicTrack (feMusic, true);
	}

	// Use this for initialization
	void Start () {
		PlayMainMenuMusic ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void FadeMusicTrack (AudioSource track, bool on) {
	// dir 0 = fade out, dir 1 = fade up 

		Debug.Log (track.volume);
		if (!on) {
			//if on is set to false 
			while (track.volume > 0) {
				// then we want to work towards zero
				track.volume = track.volume - (Time.deltaTime / (fadeTime + 1));
			}
			// and then stop the track entirely
			track.Stop ();
		} else {
			//else start playing the track
			track.Play ();
			//and approach volume of one
			while (track.volume < 1) {
				track.volume = track.volume + (Time.deltaTime / (fadeTime + 1));
			}
		}
	}

}