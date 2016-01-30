﻿using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class ArcViz : MonoBehaviour {
    private LineRenderer _renderer;

    [Header("Debug Vars:")] [Range(0f, 100f)] 
    [SerializeField] private float _power = 30f;
    [SerializeField] private bool _debug;

    [Header("Settings:")]
    [Range(0.01f, 1f)]
    [SerializeField] private float _timeStep = 0.5f;
    [SerializeField] private float _maxTime = 10f;

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

    private void Update() {
        if (_debug)
            DrawTrajectory(transform.position, transform.forward, _power);
    }

    private Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time) {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    private void PlotTrajectory(Vector3 start, Vector3 StartVelocity, float timeStep, float maxTime) {
        var length = Mathf.RoundToInt(maxTime / timeStep);
        _renderer.SetVertexCount(length + 1);

        var prev = start;
        _renderer.SetPosition(0, start);
        for (int i = 1;; i++) {
            float t = timeStep * i;
            if (t > maxTime)
                break;
            var pos = PlotTrajectoryAtTime(start, StartVelocity, t);
            Debug.DrawLine(prev, pos, Color.red);
            _renderer.SetPosition(i, pos);
            prev = pos;
        }
    }

    public void DrawTrajectory(Vector3 start, Vector3 startVelocity, float power) {
        _renderer.enabled = true;
        PlotTrajectory(start, startVelocity * power, TimeStep, MaxTime);
    }

    public void ClearTrajectory() {
        _renderer.enabled = false;
    }
}
