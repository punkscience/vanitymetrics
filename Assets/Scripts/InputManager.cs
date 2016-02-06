using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {
    [SerializeField] private float _inputScaling = 2f;

    [SerializeField]
    private ShowRange onTouch = new ShowRange();

	[SerializeField]
	private ShowRange showRange = new ShowRange();

	[SerializeField]
	private HideRange hideRange = new HideRange();
    private Vector3 _orgLoc;

	private void Update () {
		if (HUDManager.isPaused)
	        return;

	    if (Input.GetMouseButtonDown(0)) {
	        _orgLoc.x = Input.mousePosition.x / (Screen.width / _inputScaling);
			if (HUDManager.invertXAxis) {
				_orgLoc.x = 4 - _orgLoc.x;
			}
			if (!HUDManager.invertYAxis) {
				_orgLoc.y = 4 - _orgLoc.y;
			}
			_orgLoc.y = Input.mousePosition.y / (Screen.height / _inputScaling);
			if (_orgLoc.y < 3.5F && Time.deltaTime > 0) {
				showRange.Invoke (Vector2.zero);
				onTouch.Invoke (Vector2.zero);
			}
	    }
        else if (Input.GetMouseButton(0)) {
	        var pos = Input.mousePosition;
			pos.x /= Screen.width / _inputScaling;
			if (HUDManager.invertXAxis) {
				pos.x = 4 - pos.x;
			}
			if (!HUDManager.invertYAxis) {
				_orgLoc.y = 4 - _orgLoc.y;
			}
	        pos.y /= Screen.height / _inputScaling;
			if (pos.y < 3.5F && Time.deltaTime > 0) {
				var diff = pos - _orgLoc;
				diff.x = Mathf.Clamp (diff.x, -1f, 1f);
				diff.y = Mathf.Clamp (diff.y, -1f, 1f);
				showRange.Invoke (diff);
			}
        }
        else if (Input.GetMouseButtonUp(0)) {
	        var pos = Input.mousePosition;
			pos.x /= Screen.width / _inputScaling;
			if (HUDManager.invertXAxis) {
				pos.x = 4 - pos.x;
			}
			if (!HUDManager.invertYAxis) {
				_orgLoc.y = 4 - _orgLoc.y;
			}
	        pos.y /= Screen.height / _inputScaling;
			if (pos.y < 3.5F && Time.deltaTime > 0) {
				var diff = pos - _orgLoc;
				diff.x = Mathf.Clamp (diff.x, -1f, 1f);
				diff.y = Mathf.Clamp (diff.y, -1f, 1f);
				showRange.Invoke (diff);
				hideRange.Invoke ();
			}
        }
	}
}


[System.Serializable]
public class ShowRange : UnityEvent<Vector2> { }

[System.Serializable]
public class HideRange : UnityEvent { }
