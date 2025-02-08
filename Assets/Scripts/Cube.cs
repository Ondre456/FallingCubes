using System;
using UnityEngine;

[RequireComponent(typeof(Repainter))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(DestructionTimer))]
public class Cube : MonoBehaviour
{
    private Repainter _repainter;
    private bool _isCollisionOccured;
    private Rigidbody _body;
    private float _timeToDestroy;
    private DestructionTimer _destructionTimer;

    public event Action<Cube> Deactivated;

    private void Awake()
    {
        _repainter = GetComponent<Repainter>();
        _body = GetComponent<Rigidbody>();
        _destructionTimer = GetComponent<DestructionTimer>();
    }

    private void OnEnable()
    {
        _destructionTimer.TimeUntilDestructionExpired += Deactivate;
    }

    private void OnDisable()
    {
        _destructionTimer.TimeUntilDestructionExpired -= Deactivate;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            if (_isCollisionOccured == false)
            {
                _destructionTimer.ActivateDestruction();
                _repainter.ChangeColorToRandom();
                _isCollisionOccured = true;
            }
        }
    }

    public void SetZeroSpeed()
    {
        _body.velocity = Vector3.zero;
    }

    private void Deactivate()
    {
        Deactivated?.Invoke(this);
        _isCollisionOccured = false;
    }
}
