using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    [SerializeField] private float _killY = -15f;
    private void Update() {
        if (transform.position.y <= _killY) {
            Destroy(gameObject);
        }
    }
}
