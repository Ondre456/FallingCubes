using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Repainter))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SelfDestructor))]
public class Cube : MonoBehaviour
{
    private Repainter _repainter;
    private bool _isCollisionOccured;
    private Rigidbody _body;
    private float _timeToDestroy;
    private SelfDestructor _destrucor;

    public event Action<Cube> OnCubeDeactivated;

    private void Awake()
    {
        _repainter = GetComponent<Repainter>();
        _body = GetComponent<Rigidbody>();
        _destrucor = GetComponent<SelfDestructor>();
        _destrucor.OnDestroy += Deactivate;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Platform platform;

        if (collision.gameObject.TryGetComponent<Platform>(out platform))
        {
            if (_isCollisionOccured == false)
            {
                _repainter.ChangeColorToRandom();
                _isCollisionOccured = true;
            }

            _destrucor.ActivateDestruction();
        }
    }

    public void SetZeroSpeed()
    {
        _body.velocity = Vector3.zero;
    }

    private void Deactivate()
    {
        OnCubeDeactivated.Invoke(this);
        OnCubeDeactivated = null;
        _isCollisionOccured = false;
    }
}
