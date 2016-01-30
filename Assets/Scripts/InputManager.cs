using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

	[SerializeField]
	private ShowRange showRange;

	[SerializeField]
	private HideRange hideRange;

	private float screenMaxX = 300f;
	private float screenMaxY = 225f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		Vector3 origin = new Vector3( 0f, 0f, 0f );
		Vector3 curPos = new Vector3( 0f, 0f, 0f );
		
		if (Input.touchSupported && Input.touchCount > 0) {

			//Store the first touch detected.
			Touch touch = Input.touches [0];

			//Check if the phase of that touch equals Began
			if (touch.phase == TouchPhase.Began) {
				//If so, set touchOrigin to the position of that touch
//				touchOrigin = touch.position;
			} else {
				curPos = touch.position;
			}
		} else {
			if (Input.GetButtonDown("Fire1")) {
				origin = Input.mousePosition;
				Debug.Log ( "Point: X:" + origin.x + " Y:" + origin.y );
			}

			if (Input.GetMouseButton (0)) {
				curPos = Input.mousePosition;

				float diffx = curPos.x - origin.x;
				float diffy = curPos.y - origin.y;

				float xpos = (diffx - (screenMaxX/2f))/(screenMaxX/2);
				float ypos = (diffy - (screenMaxY / 2f)) / (screenMaxY / 2);
				showRange.Invoke( new Vector2( xpos, ypos ) );
			}

			if (Input.GetButtonUp ("Fire1")) {
				hideRange.Invoke ();
			}
		}
	}
}


public class ShowRange : UnityEvent<Vector2> {
	
}

public class HideRange : UnityEvent {

}
