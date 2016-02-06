using UnityEngine;
using UnityEngine.UI;

public class UnScaledTimeParticle : MonoBehaviour {
    [SerializeField] private Toggle _toggle;
    private ParticleSystem _particles;

    private void Awake() {
        _particles = GetComponent<ParticleSystem>();
    }

    private void Update() {
        _particles.Simulate(Time.unscaledDeltaTime, true, false);
    }

    private void OnEnable() {
        if (_toggle.isOn)
            _particles.Play(true);
    }
}
