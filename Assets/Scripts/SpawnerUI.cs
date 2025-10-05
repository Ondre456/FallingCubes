using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerUI : MonoBehaviour
{
    [SerializeField] private RainPool _pool;
    [SerializeField] private TextMeshProUGUI _totalSpawnedTextBox;
    [SerializeField] private TextMeshProUGUI _countOfCreatedObjectsTextBox;
    [SerializeField] private TextMeshProUGUI _countOfActiveObjectsTextBox;

    private void FixedUpdate()
    {
        _totalSpawnedTextBox.text = _pool.TotalSpawnedObjectsCount.ToString();
        _countOfActiveObjectsTextBox.text = _pool.CountOfActiveObjects.ToString();
        _countOfCreatedObjectsTextBox.text = _pool.CountOfCreatedObjects.ToString();
    }
}
