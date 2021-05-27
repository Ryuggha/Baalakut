using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHitBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9) collision.gameObject.GetComponentInParent<PlayerManager>().takeDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) other.gameObject.GetComponentInParent<PlayerManager>().takeDamage();
    }
}
