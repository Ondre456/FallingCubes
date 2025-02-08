using System;
using System.Collections;
using UnityEngine;

public class DestructionTimer : MonoBehaviour
{
    public event Action OnTimeUntilDestructionExpired;

    public void ActivateDestruction()
    {
        const float MinTimeToDestroy = 2f;
        const float MaxTimeToDestroy = 5f;

        float timeToDestroy = UnityEngine.Random.Range(MinTimeToDestroy, MaxTimeToDestroy);

        StartCoroutine(CountDownToSelfDestruction(timeToDestroy));
    }

    private IEnumerator CountDownToSelfDestruction(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);

        OnTimeUntilDestructionExpired();
    }
}
