using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleCards : MonoBehaviour {

	public GameObject[] titleCards;
	public float startDelay = 2.0f;
	public uint[] cardDelays;
	public AudioSource[] sources;
	private uint currentTitleCard = 0;
	private AudioSource audio;


	// Use this for initialization
	void Start () {
		DoCards();
	}
	
	// Update is called once per frame
	void Update () {
	}


	void DoCards() {
		var tween = TweenAlpha.Tween (titleCards[currentTitleCard], 2.0f, 1f, DisplayCard );
		tween.delay = startDelay;
	}


	void DisplayCard() {
		// Play the audio
		if( sources[currentTitleCard] != null ) {
			audio = sources[currentTitleCard];
			audio.Play ();
		}

		// Fade out this card after the delay
		var tween = TweenAlpha.Tween (titleCards[currentTitleCard], 2.0f, 0f, NextCard );
		tween.delay = cardDelays[currentTitleCard];
	}

	void NextCard() {
		if (currentTitleCard+1 == titleCards.Length) {
//			SceneManager.LoadScene ( 1 );
			return; // We're done.
		}

		var tween = TweenAlpha.Tween( titleCards[++currentTitleCard], 2.0f, 1f, DisplayCard );
		tween.delay = 2.0f;
	}

}
