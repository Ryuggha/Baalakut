using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHitBox : MonoBehaviour
{
    public bool lavaSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (lavaSound) SoundHandler.playSound("event:/SFX/Character/LavaSound", collision.gameObject.transform.position);
            collision.gameObject.GetComponentInParent<PlayerManager>().instaKill();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (lavaSound) SoundHandler.playSound("event:/SFX/Character/LavaSound", other.gameObject.transform.position);
            other.gameObject.GetComponentInParent<PlayerManager>().instaKill();
        }
    }
}
