using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    private void OnTr(Collision collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == 12)
        {
            SoundHandler.playSound("event:/SFX/Shadow/ShadowStep", transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            SoundHandler.playSound("event:/SFX/Shadow/ShadowStep", transform.position);
        }
    }
}
