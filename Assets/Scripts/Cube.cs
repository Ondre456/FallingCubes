using System;
using UnityEngine;

[RequireComponent(typeof(Repainter))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(CubeImpactLogger))]
public class Cube : SpawnableObject
{
    private Repainter _repainter;
    private float _timeToDestroy;

    private void Awake()
    {
        base.Awake();
        _repainter = GetComponent<Repainter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            if (IsCollisionOccured == false)
            {
                DestructionTimer.ActivateDestruction();
                _repainter.ChangeColorToRandom();
                IsCollisionOccured = true;
            }
        }
    }

}

public class CubeImpactLogger : MonoBehaviour
{
    public void OnImpact()
    {
        Debug.Log($"Куб {gameObject.name} получил удар");
    }
}