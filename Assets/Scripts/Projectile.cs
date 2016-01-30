using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    [SerializeField] private float _killY = -15f;

    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        transform.forward = _rb.velocity.normalized;

        if (transform.position.y <= _killY) {
            Destroy(gameObject);
        }
    }
}
