using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    public event Action OnDestroy;

    public void ActivateDestruction()
    {
        const float MinTimeToDestroy = 2f;
        const float MaxTimeToDestroy = 5f;

        float timeToDestroy = UnityEngine.Random.Range(MinTimeToDestroy, MaxTimeToDestroy + 1);

        StartCoroutine(DeactivateAfterDelay(timeToDestroy));
    }

    private IEnumerator DeactivateAfterDelay(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);

        OnDestroy();
    }
}
