using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrigger : MonoBehaviour
{
    private bool hasStoped;
    private PlayerManager player;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 3 && !hasStoped) player.darknessTrigger();
    }

    public void stop()
    {
        GetComponent<ParticleSystem>().Stop();
        hasStoped = true;
        Destroy(gameObject, 6);
    }
}
