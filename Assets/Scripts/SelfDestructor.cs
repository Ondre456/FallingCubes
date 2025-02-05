using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class SelfDestructor : MonoBehaviour
{
    private float _timeToDestroy;
    private Cube _cube;

    private void Awake()
    {
        const float MinTimeToDestroy = 2f;
        const float MaxTimeToDestroy = 5f;

        _timeToDestroy = Random.Range(MinTimeToDestroy, MaxTimeToDestroy);
        _cube = GetComponent<Cube>();

        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        _cube.Deactivate();
    }
}
