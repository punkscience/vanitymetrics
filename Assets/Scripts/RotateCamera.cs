using System.Collections;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
    [SerializeField] private float _maxAngleX = 30f;
    [SerializeField] private float _maxAngleY = 30f;
    private Quaternion _startRotation;

    [SerializeField] private float _lerpTime = 0.6f;

    [SerializeField] private Vector3 _bowOnScreen;
    [SerializeField] private Vector3 _bowOffScreen;

    private void Awake() {
        _startRotation = transform.rotation;
    }

    public void Rotate(Vector2 value) {
        var angles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(_startRotation.eulerAngles.x + (value.y * _maxAngleY),
            value.x * _maxAngleX, angles.z);
    }

    public void Release() {
        StartCoroutine(LerpCamera(transform.rotation, _startRotation, _lerpTime));
    }

    private IEnumerator LerpCamera(Quaternion from, Quaternion to, float time) {
        var elaspedTime = 0f;
        while (elaspedTime < time) {
            var easedTime = Easing.Quadratic.Out(elaspedTime / time);
            transform.rotation = Quaternion.Slerp(from, to, easedTime);
            elaspedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = _startRotation;
    }
}
