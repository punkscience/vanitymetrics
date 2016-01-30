using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	[SerializeField]
	private AudioSource feMusic;

	[SerializeField]
	private AudioSource ambience;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void StartMainMenuMusic () {
		StartCoroutine (FadeInAudio (feMusic));
	}
		

	public void TransitionToAmbient() {
		StartCoroutine (FadeOutAudio (feMusic));
		StartCoroutine (FadeInAudio (ambience));
	}

	public static IEnumerator FadeOutAudio (AudioSource audioSource) {

		float startVolume = audioSource.volume;
		float fadeTime = 0.1f;

			//if we're fading out
			while (audioSource.volume > 0) {
				audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
				yield return null;
			}
			audioSource.Stop ();

	}

	public static IEnumerator FadeInAudio (AudioSource audioSource) {

		float startVolume = audioSource.volume;
		float fadeTime = 0.1f;

		//if we're fading out
		while (audioSource.volume < 1) {
			audioSource.volume += startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
		audioSource.Stop ();

	}
}