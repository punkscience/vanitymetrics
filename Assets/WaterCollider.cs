using UnityEngine;
using System.Collections;

public class WaterCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {

		// Did we collide with an arrow
		if (other.gameObject.layer == 9) {
			AudioSource splash = GetComponent<AudioSource> ();
			splash.Play ();
		}
	}
}
