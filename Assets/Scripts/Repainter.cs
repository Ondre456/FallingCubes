using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Repainter : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor = Color.gray;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColorToRandom()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        _renderer.material.color = randomColor;
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
}
