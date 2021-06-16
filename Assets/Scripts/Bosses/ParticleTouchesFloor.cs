using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTouchesFloor : MonoBehaviour
{
    public ParticleSystem desintegratingParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 8)
        {
            Invoke("destroy", 2f);
        }
    }

    private void destroy()
    {
        Instantiate(desintegratingParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
