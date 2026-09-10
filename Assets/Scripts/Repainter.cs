using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Repainter : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.gray;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        
        if (_renderer == null)
        {
            Debug.LogError($"Renderer not found on {gameObject.name}");
        }
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

    public void StartAlphaChannelChanging(float duration)
    {
        StartCoroutine(AlphaFadeRoutine(duration));
    }

    private IEnumerator AlphaFadeRoutine(float duration)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null || renderer.material == null)
            yield break;

        Material mat = renderer.material;

        Color color = mat.color;
        float startAlpha = color.a;
        float targetAlpha = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            //Debug.Log(newAlpha);
            color.a = newAlpha;
            mat.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = targetAlpha;
        mat.color = color;
    }
}
