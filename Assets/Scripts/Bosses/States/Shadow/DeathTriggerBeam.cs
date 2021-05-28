using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerBeam : MonoBehaviour
{
    public PlayerManager player;

    void Start()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1) player.takeDamage();
        Destroy(gameObject);
    }
}
