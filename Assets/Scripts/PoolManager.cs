using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolManager<T> : MonoBehaviour, IObservablePool where T : SpawnableObject
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolCapacity = 5;
    [SerializeField] protected int _poolMaxSize = 100;

    protected ObjectPool<T> _pool;

    private int _countOfCreatedObjects;
    private int _countOfActiveObjects;
    private int _countOfTotalSpawnedObjects;

    public int CountOfCreatedObjects
    {
        get => _countOfCreatedObjects;

        protected set
        {
            _countOfCreatedObjects = value;
            OnCountOfCreatedObjectsChanged?.Invoke(value);
        }
    }

    public int CountOfActiveObjects
    {
        get => _countOfActiveObjects;

        protected set
        {
            _countOfActiveObjects = value;
            OnCountOfActiveObjectsChanged?.Invoke(value);
        }
    }

    public int TotalSpawnedObjectsCount
    {
        get => _countOfTotalSpawnedObjects;

        protected set
        {
            _countOfTotalSpawnedObjects = value;
            OnTotalSpawnedObjectsCountChanged?.Invoke(value);
        }
    }

    public event Action<int> OnCountOfCreatedObjectsChanged;
    public event Action<int> OnCountOfActiveObjectsChanged;
    public event Action<int> OnTotalSpawnedObjectsCountChanged;

    protected void Awake()
    {
        InitializePool();
    }

    protected virtual void InitializePool()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateFunction,
            actionOnGet: ActionOnGet,
            actionOnRelease: ActionOnRelease,
            actionOnDestroy: ActionOnDestroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    protected abstract T CreateFunction();
    protected abstract void ActionOnGet(T obj);

    protected virtual void ActionOnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        OnDeactivated(obj);
    }

    protected virtual void ActionOnDestroy(T obj)
    {
        Destroy(obj.gameObject);
    }

    protected abstract void OnDeactivated(T obj);
}
