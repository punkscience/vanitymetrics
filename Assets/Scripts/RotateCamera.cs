using UnityEngine;

public class RotateCamera : MonoBehaviour {
    [SerializeField] private float _maxAngleX = 30f;
    [SerializeField] private float _maxAngleY = 30f;
    private Quaternion _startRotation;

    private void Awake() {
        _startRotation = transform.rotation;
    }

    public void Rotate(Vector2 value) {
        var angles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(_startRotation.eulerAngles.x + (value.y * _maxAngleY),
            value.x * _maxAngleX, angles.z);
    }

    public void Release() {
        transform.rotation = _startRotation;
    }
}
