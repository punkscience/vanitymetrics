using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcViz : MonoBehaviour {
    private LineRenderer _renderer;

    [Range(0f, 100f)]
    [SerializeField] private float _power = 30f;
    [Range(0.01f, 1f)]
    [SerializeField] private float _timeStep = 0.5f;
    [SerializeField] private float _maxTime = 10f;

    private void Awake() {
        _renderer = GetComponent<LineRenderer>();
    }

    private void Update() {
        PlotTrajectory(Vector3.zero, transform.forward * _power, _timeStep, _maxTime);
    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time) {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector3 start, Vector3 StartVelocity, float timeStep, float maxTime) {
        var length = Mathf.RoundToInt(maxTime / timeStep);
        _renderer.SetVertexCount(length + 1);

        var prev = start;
        _renderer.SetPosition(0, start);
        for (int i = 1;; i++) {
            float t = timeStep * i;
            if (t > maxTime)
                break;

            var pos = PlotTrajectoryAtTime(start, StartVelocity, t);

            if (Physics.Linecast(prev, pos))
                break;

            Debug.DrawLine(prev, pos, Color.red);
            _renderer.SetPosition(i, pos);
            prev = pos;
        }
    }
}
