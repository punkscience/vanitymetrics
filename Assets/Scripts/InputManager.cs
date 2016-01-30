using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

	[SerializeField]
	private ShowRange showRange = new ShowRange();

	[SerializeField]
	private HideRange hideRange = new HideRange();

    private Vector2 _orgLoc;

	void Update () {
	    if (Input.GetMouseButtonDown(0)) {
	        _orgLoc = Input.mousePosition;
            showRange.Invoke(Vector2.zero);
	    }
        else if (Input.GetMouseButton(0)) {
            Vector2 pos = new Vector2(_orgLoc.x - (Input.mousePosition.x / Screen.width),
                Input.mousePosition.y - (_orgLoc.y / Screen.height));
            showRange.Invoke(pos);
        }
        else if (Input.GetMouseButtonUp(0)) {
            Vector2 pos = new Vector2(_orgLoc.x - (Input.mousePosition.x / Screen.width),
                Input.mousePosition.y - (_orgLoc.y / Screen.height));
            showRange.Invoke(pos);
            hideRange.Invoke();
        }
	}
}


[System.Serializable]
public class ShowRange : UnityEvent<Vector2> { }

[System.Serializable]
public class HideRange : UnityEvent { }
