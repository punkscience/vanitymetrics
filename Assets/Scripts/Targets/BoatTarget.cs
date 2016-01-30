using UnityEngine;
using System.Collections;

public class BoatTarget : MonoBehaviour {

	[SerializeField]
	protected float boatSpeed = 0;

	public void Init (float angle, float speed) {
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = new Vector3 (0, angle, 0);
		boatSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * (boatSpeed * Time.deltaTime);
	}
}
