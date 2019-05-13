﻿using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class DuckMovement : MonoBehaviour {

    public EndOfPathInstruction _endOfPathInstruction;
    public float _deathDstToEndPoint;
    VertexPath _path;
    float _distanceTravelled;
    float _speed;

    Vector3 _endPoint;

    DuckHealth _health;

    void Awake() {
        _health = GetComponent<DuckHealth>();
    }

    public void Initialize(float speed, VertexPath path) {
        _speed = speed;
        _path = path;

        _endPoint = path.vertices[path.NumVertices - 1];
    }

    void Update() {
        _distanceTravelled += _speed * Time.deltaTime;
        transform.position = _path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);

        var forward = _path.GetDirectionAtDistance(_distanceTravelled, _endOfPathInstruction);
        var rot = Quaternion.LookRotation(forward,Vector3.up);

        transform.rotation = rot;

        if (Vector3.SqrMagnitude(_endPoint - transform.position) <= _deathDstToEndPoint * _deathDstToEndPoint) {
            // Just for now, change to Flee later
            _health.Death();
        }
    }
}