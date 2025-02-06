using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RainPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private float _repeatRate = 0.3f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private int _areaSize = 45;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _spawnPoint = transform.position;
        _pool = new ObjectPool<Cube>(
                createFunc: () => CreateFunction(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        StartCoroutine(GetCubeCoroutine());
    }

    private void ActionOnGet(Cube obj)
    {
        Vector3 newPosition = _spawnPoint;

        newPosition.x += Random.Range(-_areaSize, _areaSize + 1);
        newPosition.z += Random.Range(-_areaSize, _areaSize + 1);

        obj.transform.position = newPosition;
        obj.OnCubeDeactivated += Obj_OnCubeDeactivated;
        obj.SetZeroSpeed();
        obj.GetComponent<Repainter>().SetDefaultColor();
        obj.gameObject.SetActive(true);
    }

    private void Obj_OnCubeDeactivated(Cube releasableCube)
    {
        _pool.Release(releasableCube);
    }

    private Cube CreateFunction()
    {
        Cube instance = Instantiate(_prefab);

        MeshRenderer meshRenderer;
        
        if (instance.TryGetComponent<MeshRenderer>(out meshRenderer))
        {
            meshRenderer.enabled = true;
        }

        Collider collider;
        
        if (instance.TryGetComponent<Collider>(out collider))
        {
            collider.enabled = true;
        }

        return instance;
    }

    private IEnumerator GetCubeCoroutine()
    {
        while (enabled)
        {
            for (int i = 0; i < 10; i++)
                GetCube();

            yield return new WaitForSeconds(_repeatRate + 1);
        }
    }

    private Cube GetCube()
    {
        return _pool.Get();
    }
}
