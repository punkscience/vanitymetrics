using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {
    [SerializeField] private float _inputScaling = 2f;

	[SerializeField]
	private ShowRange showRange = new ShowRange();

	[SerializeField]
	private HideRange hideRange = new HideRange();
    private Vector3 _orgLoc;

	private void Update () {
	    if (Input.GetMouseButtonDown(0)) {
	        _orgLoc.x = Input.mousePosition.x / (Screen.width / _inputScaling);
	        _orgLoc.y = Input.mousePosition.y / (Screen.height / _inputScaling);
	        var pos = Input.mousePosition;
	        pos.x /= Screen.width * _inputScaling;
	        pos.y /= Screen.height * _inputScaling;
            var diff = pos - _orgLoc;
            showRange.Invoke(Vector2.ClampMagnitude(diff, 1f));
	    }
        else if (Input.GetMouseButton(0)) {
	        var pos = Input.mousePosition;
            pos.x /= Screen.width / _inputScaling;
	        pos.y /= Screen.height / _inputScaling;
            var diff = pos - _orgLoc;
            showRange.Invoke(Vector2.ClampMagnitude(diff, 1f));
        }
        else if (Input.GetMouseButtonUp(0)) {
	        var pos = Input.mousePosition;
	        pos.x /= Screen.width / _inputScaling;
	        pos.y /= Screen.height / _inputScaling;
            var diff = pos - _orgLoc;
            showRange.Invoke(Vector2.ClampMagnitude(diff, 1f));
            hideRange.Invoke();
        }
	}
}


[System.Serializable]
public class ShowRange : UnityEvent<Vector2> { }

[System.Serializable]
public class HideRange : UnityEvent { }
