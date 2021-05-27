using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornGeneratorParticles : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerManager>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * 4);
        //transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 15);
    }
}
