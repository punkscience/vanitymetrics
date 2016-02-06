using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleCards : MonoBehaviour {

	public GameObject[] titleCards;
	public float startDelay = 2.0f;
	public uint[] cardDelays;
	public AudioSource[] sources;
	private uint currentTitleCard = 0;
	private bool fadeOut;
	private bool canceledFade;

	// Use this for initialization
	void Start () {
		DoCards();
	}
	
	// Update is called once per frame
	void Update () {
	}


	void DoCards() {
		StartTween (false);

//		var tween = TweenAlpha.Tween (titleCards[currentTitleCard], 2.0f, 1f, DisplayCard );
//		tween.delay = startDelay;
	}


	void DisplayCard() {
		// Play the audio
		if( sources[currentTitleCard] != null ) {
			AudioSource clip = sources[currentTitleCard];
			clip.Play ();
		}

		// Fade out this card after the delay

		StartTween (true);
//		var tween = TweenAlpha.Tween (titleCards[currentTitleCard], 2.0f, 0f, NextCard );
//		tween.delay = cardDelays[currentTitleCard];
	}

	void StartTween (bool fadeOut) {
		
		float delay = cardDelays [currentTitleCard];
		Graphic img = titleCards [currentTitleCard].GetComponent<Graphic> ();

		if (fadeOut) {
			img.color = new Color (1, 1, 1, 1.0F);
		} else {
			img.color = new Color (1, 1, 1, 0.0F);
		}

		StartCoroutine (Tween (img, delay, fadeOut));
	}

	IEnumerator Tween (Graphic img, float delay, bool fadeOut) {

		// increment the delay timer
		float timer = 0.0F;
		bool cancelFadeIn = false;

		// wait for the delay
		while (timer < delay) {
			timer += Time.deltaTime;
			if (canceledFade) {
				break;
			}
			yield return null;
		}

		if (!cancelFadeIn) {
			timer = 0.0F;
			while (timer < 1.0F) {
				img.color = new Color (1, 1, 1, (fadeOut ? (1.0F - timer) : timer));

				if (canceledFade) {

					// if the user cancelled during the fade in only cancel the fade in
					if (!fadeOut) {
						img.color = new Color (1, 1, 1, 1.0F);
						cancelFadeIn = true;
						canceledFade = false;
					}
					break;
				}

				timer += Time.deltaTime / 2.0F;
				yield return null;
			}
		}

		// if the player canceled skip to the next card
		if (canceledFade) {
			img.color = new Color (1, 1, 1, 0.0F);
			canceledFade = false;
			IncrementCard ();
			DisplayCard ();
		} else if (fadeOut) {
			NextCard ();
		} else {
			DisplayCard ();
		}
	}

	void IncrementCard () {
		if (currentTitleCard + 1 == titleCards.Length) {
			SceneManager.LoadScene ("FrontEnd", LoadSceneMode.Single);
			return; // We're done.
		}

		currentTitleCard++;
	}

	void NextCard() {
		IncrementCard ();
		StartTween (false);
//		var tween = TweenAlpha.Tween( titleCards[++currentTitleCard], 2.0f, 1f, DisplayCard );
//		tween.delay = 2.0f;
	}

	public void SkipCards() {
		if (sources [currentTitleCard] != null) {
			sources [currentTitleCard].Stop ();
		}
		canceledFade = true;
//		SceneManager.LoadScene ("FrontEnd", LoadSceneMode.Single);
	}


}
