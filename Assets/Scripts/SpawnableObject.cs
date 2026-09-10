using System;
using UnityEngine;

[RequireComponent(typeof(DestructionTimer))]
[RequireComponent(typeof(Rigidbody))]
public abstract class SpawnableObject : MonoBehaviour
{
    protected bool IsCollisionOccured;
    protected DestructionTimer DestructionTimer;
    private Rigidbody _body;

    public event Action<SpawnableObject> Deactivated;

    protected void Awake()
    {
        _body = GetComponent<Rigidbody>();
        DestructionTimer = GetComponent<DestructionTimer>();
    }

    private void OnEnable()
    {
        DestructionTimer.TimeUntilDestructionExpired += Deactivate;
    }

    private void OnDisable()
    {
        DestructionTimer.TimeUntilDestructionExpired -= Deactivate;
    }

    protected virtual void Deactivate()
    {
        Deactivated?.Invoke(this);
        IsCollisionOccured = false;
    }

    public void SetZeroSpeed()
    {
        _body.velocity = Vector3.zero;
    }
}
