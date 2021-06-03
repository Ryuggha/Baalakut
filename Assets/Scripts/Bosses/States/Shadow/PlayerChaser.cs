using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    private Transform player;
    public float velocity = 7.1f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerManager>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, velocity * Time.fixedDeltaTime);
    }
}
