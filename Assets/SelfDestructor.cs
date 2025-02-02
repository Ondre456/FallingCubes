using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    private float _timeToDestroy;
    private float _timer = 0;

    private void Awake()
    {
        _timeToDestroy = Random.Range(2f, 5f);
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer >= _timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
