using UnityEngine;

public class RotateCamera : MonoBehaviour {
    private Quaternion _startRotation;

    private void Awake() {
        _startRotation = transform.rotation;
    }

    public void Rotate(Vector2 value) {
        transform.Rotate(Vector3.up * value.x);
    }

    public void Release() {
        transform.rotation = _startRotation;
    }
}
