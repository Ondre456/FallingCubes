using Assets.Scripts;
using TMPro;
using UnityEngine;

public class SpawnedObjectsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalSpawnedTextBox;
    [SerializeField] private TextMeshProUGUI _countOfCreatedObjectsTextBox;
    [SerializeField] private TextMeshProUGUI _countOfActiveObjectsTextBox;
    [SerializeField] private string _objectsNameGenitiveCase;

    private IObservablePool _pool;


    private string _totalSpawnedCount = "Общее количество созданных ";
    private string _countOfCreatedObjects = "Количество созданных ";
    private string _countOfActiveObjects = "Количество активных ";

    public void Initialize(IObservablePool pool)
    {
        _pool = pool;

        SubscribeToPoolEvents();
        UpdateInitialValues();
    }

    private void Awake()
    {
        _totalSpawnedCount = $"Общее количество созданных {_objectsNameGenitiveCase} ";
        _countOfCreatedObjects = $"Количество созданных {_objectsNameGenitiveCase} ";
        _countOfActiveObjects = $"Количество активных {_objectsNameGenitiveCase} ";

        SubscribeToPoolEvents();
        UpdateInitialValues();
    }

    private void OnDestroy()
    {
        UnsubscribeFromPoolEvents();
    }

    private void UpdateTotalSpawnedText(int count)
    {
        _totalSpawnedTextBox.text = _totalSpawnedCount + count.ToString();
    }

    private void UpdateCreatedCountText(int count)
    {
        _countOfCreatedObjectsTextBox.text = _countOfCreatedObjects + count.ToString();
    }

    private void UpdateActiveCountText(int count)
    {
        _countOfActiveObjectsTextBox.text = _countOfActiveObjects + count.ToString();
    }

    private void SubscribeToPoolEvents()
    {
        _pool.OnCountOfCreatedObjectsChanged += UpdateCreatedCountText;
        _pool.OnCountOfActiveObjectsChanged += UpdateActiveCountText;
        _pool.OnTotalSpawnedObjectsCountChanged += UpdateTotalSpawnedText;
    }

    private void UnsubscribeFromPoolEvents()
    {
        if (_pool != null)
        {
            _pool.OnCountOfCreatedObjectsChanged -= UpdateCreatedCountText;
            _pool.OnCountOfActiveObjectsChanged -= UpdateActiveCountText;
            _pool.OnTotalSpawnedObjectsCountChanged -= UpdateTotalSpawnedText;
        }
    }

    private void UpdateInitialValues()
    {
        _totalSpawnedTextBox.text = _totalSpawnedCount + _pool.TotalSpawnedObjectsCount.ToString();
        _countOfCreatedObjectsTextBox.text = _countOfCreatedObjects + _pool.CountOfCreatedObjects.ToString();
        _countOfActiveObjectsTextBox.text = _countOfActiveObjects + _pool.CountOfActiveObjects.ToString();
    }
}
