using UnityEngine;
using System.Collections;

public class DemonTarget : MonoBehaviour {

	protected float demonHealth;
	protected float demonSpeed;
	protected Transform demonTarget;

	public void Init (float angle, float health, float speed, Transform startTransform, Transform target) {

		transform.position = startTransform.position;
		transform.localEulerAngles = new Vector3 (0, angle + 180.0F, 0);

		demonHealth = health;
		demonSpeed = speed;
		demonTarget = target;
	}
	
	// Update is called once per frame
	void Update () {

//		transform.position 
	}
}
