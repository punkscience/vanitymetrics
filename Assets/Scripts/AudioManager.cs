using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	[SerializeField]
	private AudioSource feMusic;

	[SerializeField]
	private AudioSource ambience;

	private float sfxVolume = 0.7f;
	private float musicVolume = 1.0f;
	private float muting = 0f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void adjustFXVolume (float newVolume) {
		sfxVolume = newVolume;
	}

	public void adjustMusicVolume (float newVolume) {
		musicVolume = newVolume;
	}
	public void StartMainMenuMusic () {
		StartCoroutine (FadeInAudio (feMusic, musicVolume));
	}
		

	public void TransitionToAmbient() {
		StartCoroutine (FadeOutAudio (feMusic, muting));
		StartCoroutine (FadeInAudio (ambience, sfxVolume));
	}

	public static IEnumerator FadeOutAudio (AudioSource audioSource, float cap) {

		float startVolume = audioSource.volume;
		float fadeTime = 4f;

			while (audioSource.volume > cap) {
				audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
				yield return null;
			}
			audioSource.Stop ();

	}

	public static IEnumerator FadeInAudio (AudioSource audioSource, float cap) {
		audioSource.Play ();
		float startVolume = audioSource.volume;
		float fadeTime = 0.4f;

		while (audioSource.volume < cap) {
			audioSource.volume += startVolume * Time.deltaTime / fadeTime;
			yield return null;
		} 

	}
}