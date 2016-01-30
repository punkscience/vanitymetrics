using UnityEngine;

public class RotateCamera : MonoBehaviour {
    [SerializeField] private float _maxAngle = 30f;
    private Quaternion _startRotation;

    private void Awake() {
        _startRotation = transform.rotation;
    }

    public void Rotate(Vector2 value) {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, value.x * _maxAngle, transform.eulerAngles.z);
    }

    public void Release() {
        transform.rotation = _startRotation;
    }
}
