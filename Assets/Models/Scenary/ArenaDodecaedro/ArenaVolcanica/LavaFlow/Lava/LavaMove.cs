using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMove : MonoBehaviour
{
    public float velX, velY;
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveLavaX = Time.time * velX;
        float moveLavaY = Time.time * velY;
        rend.material.SetTextureOffset("_MainTex", new
                Vector2(moveLavaX, moveLavaY));
    }
}
