using UnityEngine;
using System.Collections;

public class BowMover : MonoBehaviour {
    [SerializeField] private Vector3 _offScreenCoord;
    [SerializeField] private Vector3 _onScreenCoord;
    [SerializeField] private float _duration = 0.6f;

    public void MoveOnScreen() {
        StartCoroutine(LerpPos(_offScreenCoord, _onScreenCoord, _duration));
    }

    public void MoveOffScreen() {
        StartCoroutine(LerpPos(_onScreenCoord, _offScreenCoord, _duration));
    }

    private IEnumerator LerpPos(Vector3 from, Vector3 to, float time) {
        var elaspedTime = 0f;
        while (elaspedTime < time) {
            var easedTime = Easing.Quadratic.Out(elaspedTime / time);
            transform.position = Vector3.Lerp(from, to, easedTime);
            elaspedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = to;
    }
}
