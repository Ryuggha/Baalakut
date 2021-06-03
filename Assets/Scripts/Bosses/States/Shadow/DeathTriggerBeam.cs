using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerBeam : MonoBehaviour
{
    public PlayerManager player;
    public float distance = 1;

    void Start()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= distance) player.takeDamage();
        Destroy(gameObject);
    }
}
