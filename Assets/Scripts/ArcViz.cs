using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcViz : MonoBehaviour {
    private LineRenderer _renderer;
    [SerializeField] private GameObject _particles;

    [SerializeField]
    private Animator _animator;

    [SerializeField] private GameObject _endPoint;

    [Range(0f, 100f)] 
    [SerializeField] private float _power = 30f;

    [SerializeField] private LayerMask _collisionMask;

    [Header("Settings:")]
    [Range(0.01f, 1f)]
    [SerializeField] private float _timeStep = 0.5f;
    [SerializeField] private float _maxTime = 10f;

    private List<Vector3> _arcPoints = new List<Vector3>();
    private GameObject _marker;

    [SerializeField] private float _coolDown = 1f;
    private float _nextAttack;

    public float TimeStep {
        get { return _timeStep; }
        set { _timeStep = value; }
    }

    public float MaxTime {
        get { return _maxTime; }
        set { _maxTime = value; }
    }

    private void Awake() {
        _renderer = GetComponent<LineRenderer>();
    }

    private Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time) {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    private void PlotTrajectory(Vector3 start, Vector3 StartVelocity, float timeStep, float maxTime) {
        _arcPoints.Clear();
        _arcPoints.Add(start);
        var prev = start;
        for (int i = 1;; i++) {
            float t = timeStep * i;

            if (t > maxTime)
                break;

            var pos = PlotTrajectoryAtTime(start, StartVelocity, t);
            _arcPoints.Add(pos);
            if (Physics.Linecast(prev, pos, _collisionMask)) {
                var ray = new Ray(prev, pos - prev);
                var hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit)) {
                    if (_marker != null) {
                        var loc = hit.point;
                        _marker.transform.position = new Vector3(loc.x, loc.y + 0.01f, loc.z);
                    }
                }
                break;
            }
            prev = pos;
        }
    }

    private void LateUpdate() {
        if (Time.time > _nextAttack) {
            _renderer.SetVertexCount(_arcPoints.Count);
            _renderer.SetPositions(_arcPoints.ToArray());
        }
    }

    public void DrawTrajectory(Vector3 start, Vector3 startVelocity, float power) {
        if (Time.time > _nextAttack) {
            _renderer.enabled = true;
            PlotTrajectory(start, startVelocity * power, TimeStep, MaxTime);
        }
    }

    public void DrawTrajectory(Vector2 value) {
        _animator.SetFloat("Blend", -value.y);

        DrawTrajectory(transform.position, transform.forward, _power);
    }

    [ContextMenu("Clear Debug View")]
    public void ClearTrajectory() {
        _renderer.enabled = false;
        _renderer.SetVertexCount(0);
        Destroy(_marker);

        if (Time.time > _nextAttack) {
            // fire
            _nextAttack = Time.time + _coolDown;
            GetComponent<ProjectileLauncher>().Fire(_power);
            _animator.SetTrigger("Release");
            _particles.SetActive(false);
        }
    }

    public void OnPull(Vector2 value) {
        if (Time.time > _nextAttack) {
            _marker = Instantiate(_endPoint);
            _animator.SetTrigger("Touch");
            _particles.SetActive(true);
        }
    }
}
