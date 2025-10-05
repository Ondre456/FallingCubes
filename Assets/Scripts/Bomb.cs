using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.WSA;

[RequireComponent(typeof(Repainter))]
public class Bomb : SpawnableObject
{
    [SerializeField] private float _explosionPower;
    [SerializeField] private float _explosionRadius = 5f;

    private SphereCollider _triggerCollider;
    private Repainter _repainter;

    private void Awake()
    {
        base.Awake();

        _repainter = GetComponent<Repainter>();
    }

    public void Activate()
    {
        if (TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            meshRenderer.enabled = true;
        }

        if (TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = true;
        }

        _triggerCollider = gameObject.AddComponent<SphereCollider>();
        _triggerCollider.isTrigger = true;
        _triggerCollider.radius = _explosionRadius;

        _repainter.SetDefaultColor();
        _repainter.StartAlphaChannelChanging(DestructionTimer.ActivateDestruction());
    }

    protected override void Deactivate()
    {
        Explode();
        base.Deactivate();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == this.gameObject)
                continue;

            Rigidbody rb = col.attachedRigidbody;
            
            if (rb != null)
            {
                // Вычисляем и выводим силу, которая будет применена
                Vector3 direction = col.transform.position - transform.position;
                float distance = direction.magnitude;
                float forceMagnitude = _explosionPower * (1 - distance / _explosionRadius);
                forceMagnitude = Mathf.Max(forceMagnitude, 0);

                Debug.Log($"Object: {col.gameObject}, Explosion Force (calculated): {forceMagnitude}, Distance: {distance}");

                // применяем force
                rb.AddExplosionForce(_explosionPower, transform.position, _explosionRadius);
            }
        }

        Destroy(_triggerCollider);
    }
}
