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
            SoundHandler.playSound("event:/SFX/Ambience/ParticleDisappear", transform.position);
            Invoke("destroy", 0.4f);
        }
    }

    private void destroy()
    {
        Instantiate(desintegratingParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
