using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class RainPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private float _repeatRate = 0.3f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private int _areaSize = 45;

    private ObjectPool<Cube> _cubePool;
    private ObjectPool<Bomb> _bombPool;

    public int TotalSpawnedObjectsCount { get; private set; }
    public int CountOfCreatedObjects { get; private set; }
    public int CountOfActiveObjects { get; private set; }

    private void Awake()
    {
        _spawnPoint = transform.position;
        _cubePool = new ObjectPool<Cube>(
                createFunc: () => CreateFunction(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );

        _bombPool = new ObjectPool<Bomb>(
                createFunc: () => Instantiate(_bombPrefab),
                actionOnGet: (bomb) => bomb.gameObject.SetActive(true),
                actionOnRelease: (bomb) => bomb.gameObject.SetActive(false),
                actionOnDestroy: (bomb) => Destroy(bomb),
                collectionCheck: true,
                defaultCapacity: 5,
                maxSize: 20
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
        ((SpawnableObject)obj).Deactivated += OnCubeDeactivated;
        obj.SetZeroSpeed();
        obj.TryGetComponent(out Repainter repainter);
        repainter.SetDefaultColor();
        obj.gameObject.SetActive(true);
    }

    private void ActionOnGet(Bomb bomb)
    {
        bomb.SetZeroSpeed();
        bomb.TryGetComponent(out Repainter repainter);
        repainter.SetDefaultColor();
        bomb.gameObject.SetActive(true);
    }

    private void OnCubeDeactivated(SpawnableObject releasableCube)
    {
        (releasableCube as SpawnableObject).Deactivated -= OnCubeDeactivated;
        Vector3 position = releasableCube.transform.position;
        _cubePool.Release(releasableCube as Cube);
        Bomb bomb = _bombPool.Get();
        bomb.transform.position = position;
        bomb.TryGetComponent(out Repainter repainter);
        repainter.SetDefaultColor();
        bomb.gameObject.SetActive(true);
        bomb.Activate();
        bomb.Deactivated += OnBombDeactivated;
    }

    private void OnBombDeactivated(SpawnableObject releasableBomb)
    {
        Vector3 position = releasableBomb.transform.position;
        _bombPool.Release(releasableBomb as Bomb);


        releasableBomb.Deactivated -= OnBombDeactivated;
    }

    private Cube CreateFunction()
    {
        Cube instance = Instantiate(_prefab);

        if (instance.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            meshRenderer.enabled = true;
        }

        if (instance.TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = true;
        }

        return instance;
    }

    private IEnumerator GetCubeCoroutine()
    {
        const int NumberOfGeneratedCubes = 10;
        const int Compensator = 1;

        WaitForSeconds waitForSeconds = new WaitForSeconds(_repeatRate + Compensator);

        while (enabled)
        {
            for (int i = 0; i < NumberOfGeneratedCubes; i++)
                _cubePool.Get();

            yield return waitForSeconds;
        }
    }
}
