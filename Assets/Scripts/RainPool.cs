using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RainPool : PoolManager<Cube>
{
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private float _repeatRate = 0.3f;
    [SerializeField] private int _areaSize = 45;
    [SerializeField] private BombPool _bombPool;
    
    private void Awake()
    {
        base.Awake();
        _spawnPoint = transform.position;
    }

    private void Start()
    {
        StartCoroutine(GetCubeCoroutine());
    }

    protected override Cube CreateFunction()
    {
        Cube instance = Instantiate(_prefab);

        if (instance.TryGetComponent(out MeshRenderer meshRenderer))
            meshRenderer.enabled = true;
        
        if (instance.TryGetComponent(out Collider collider))
            collider.enabled = true;

        CountOfCreatedObjects++;
        
        return instance;
    }

    protected override void ActionOnGet(Cube obj)
    {
        Vector3 newPosition = _spawnPoint;
        newPosition.x += Random.Range(-_areaSize, _areaSize + 1);
        newPosition.z += Random.Range(-_areaSize, _areaSize + 1);

        obj.transform.position = newPosition;
        obj.SetZeroSpeed();
        obj.Deactivated += OnCubeDeactivated;

        if (obj.TryGetComponent(out Repainter repainter))
            repainter.SetDefaultColor();

        obj.gameObject.SetActive(true);
        CountOfActiveObjects++;
        TotalSpawnedObjectsCount++;
    }

    protected override void OnDeactivated(Cube obj)
    {
        obj.Deactivated -= OnCubeDeactivated;
        CountOfActiveObjects--;
    }
    
    private void OnCubeDeactivated(SpawnableObject cube)
    {
        var cubeObj = cube as Cube;
        if (cubeObj == null) return;

        Vector3 bombPosition = cubeObj.transform.position;

        if (_bombPool != null)
        {
            _bombPool.ActivateBombAtPosition(bombPosition);
        }
        else
        {
            Debug.LogError("BombPool не назначен в RainPool!");
        }

        _pool.Release(cubeObj);
    }

    private IEnumerator GetCubeCoroutine()
    {
        const int NumberOfGeneratedCubes = 10;
        const int Compensator = 1;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_repeatRate + Compensator);

        while (enabled)
        {
            for (int i = 0; i < NumberOfGeneratedCubes; i++)
            {
                _pool.Get();
            }

            yield return waitForSeconds;
        }
    }
}
