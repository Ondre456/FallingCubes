using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Repainter))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> OnCubeDeactivated;
    private Repainter _repainter;
    private bool _isCollisionOccured;
    private Rigidbody _body;
    private float _timeToDestroy;
    private SelfDestructor _destrucor;

    private void Awake()
    {
        _repainter = GetComponent<Repainter>();
        _body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollisionOccured == false)
        {
            _repainter.ChangeColorToRandom();
            _isCollisionOccured = true;
        }

        gameObject.AddComponent<SelfDestructor>();
    }

    public void SetZeroSpeed()
    {
        _body.velocity = Vector3.zero;
    }

    public void Deactivate()
    {
        OnCubeDeactivated.Invoke(this);
        OnCubeDeactivated = null;
        _isCollisionOccured = false;
    }
}
