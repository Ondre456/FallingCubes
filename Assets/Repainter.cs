using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Repainter : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColorToRandom()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        Debug.Log(_renderer.material);
        _renderer.material.color = randomColor;
    }
}
