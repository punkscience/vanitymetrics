using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

	[SerializeField]
	private ShowRange showRange = new ShowRange();

	[SerializeField]
	private HideRange hideRange = new HideRange();

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
				origin = touch.position;
				curPos = touch.position;

				float diffx = curPos.x - origin.x;
				float diffy = curPos.y - origin.y;

				float xpos = (diffx - (screenMaxX/2f))/(screenMaxX/2);
				float ypos = (diffy - (screenMaxY / 2f)) / (screenMaxY / 2);

				showRange.Invoke( new Vector2( xpos, ypos ) );

			} else if (touch.phase == TouchPhase.Moved) {
				curPos = touch.position;

				float diffx = curPos.x - origin.x;
				float diffy = curPos.y - origin.y;

				float xpos = (diffx - (screenMaxX/2f))/(screenMaxX/2);
				float ypos = (diffy - (screenMaxY / 2f)) / (screenMaxY / 2);

				showRange.Invoke( new Vector2( xpos, ypos ) );

			} else if (touch.phase == TouchPhase.Ended) {
				hideRange.Invoke ();
			}
		} else {
			if (Input.GetButtonDown("Fire1")) {
				origin = Input.mousePosition;
				curPos = Input.mousePosition;
//				Debug.Log ( "Point: X:" + origin.x + " Y:" + origin.y );

				float diffx = curPos.x - origin.x;
				float diffy = curPos.y - origin.y;

				float xpos = (diffx - (screenMaxX/2f))/(screenMaxX/2);
				float ypos = (diffy - (screenMaxY / 2f)) / (screenMaxY / 2);

				showRange.Invoke( new Vector2( xpos, ypos ) );
			} else if (Input.GetMouseButton (0)) {
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
