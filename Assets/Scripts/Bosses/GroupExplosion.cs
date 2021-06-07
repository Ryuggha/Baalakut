using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupExplosion : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] float minForce = 0;
    [SerializeField] float maxForce = 5;
    [SerializeField] float explosionRadius = 10;
    [SerializeField] float destroyDelay = 5;
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float explosionEffectDestroyDelay = 2;

    void Start()
    {
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, explosionRadius);
            }
            Destroy(t.gameObject, destroyDelay);
            if (explosionParticles != null)
            {
                GameObject explosionEffect = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                Destroy(explosionEffect, explosionEffectDestroyDelay);
            }
            Destroy(gameObject, explosionEffectDestroyDelay);
        }
    }
}
