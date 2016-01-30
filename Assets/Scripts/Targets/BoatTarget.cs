using UnityEngine;
using System.Collections;

public class BoatTarget : MonoBehaviour {
	
	protected float boatSpeed = 0;

	void Init (float angle, float speed) {
		transform.localPosition = 0;
		transform.eulerAngles = new Vector3 (0, angle, 0);
		boatSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
