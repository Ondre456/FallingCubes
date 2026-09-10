using UnityEngine;

public class BombPool : PoolManager<Bomb>
{
    protected override Bomb CreateFunction()
    {
        Bomb instance = Instantiate(_prefab);

        if (instance.TryGetComponent(out MeshRenderer meshRenderer))
            meshRenderer.enabled = true;
        
        if (instance.TryGetComponent(out Collider collider))
            collider.enabled = true;

        CountOfCreatedObjects++;
        
        return instance;
    }

    protected override void ActionOnGet(Bomb obj)
    {
        obj.SetZeroSpeed();

        if (obj.TryGetComponent(out Repainter repainter))
            repainter.SetDefaultColor();

        obj.gameObject.SetActive(true);
        TotalSpawnedObjectsCount++;
    }

    protected override void OnDeactivated(Bomb obj)
    {
        CountOfActiveObjects--;
    }

    public void ActivateBombAtPosition(Vector3 position)
    {
        Bomb bomb = _pool.Get();
        bomb.transform.position = position;
        bomb.gameObject.SetActive(true);
        bomb.Activate();
        bomb.Deactivated += OnBombDeactivated;
    }

    private void OnBombDeactivated(SpawnableObject releasableBomb)
    {
        var bomb = releasableBomb as Bomb;
        _pool.Release(bomb);
        releasableBomb.Deactivated -= OnBombDeactivated;
    }
}
