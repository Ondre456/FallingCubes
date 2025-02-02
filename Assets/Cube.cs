using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Repainter))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private Repainter _repainter;
    private bool _isCollisionOccured;

    private void Awake()
    {
        _repainter = GetComponent<Repainter>();
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
}
