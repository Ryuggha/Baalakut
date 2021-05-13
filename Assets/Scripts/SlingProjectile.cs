using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingProjectile : MonoBehaviour
{
    public LayerMask layerMask;

    private Rigidbody rb;
    private Vector3 shotSpeed;
    private float gravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(shotSpeed, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Vector3 rbVel = rb.velocity;
        rbVel.y += gravity * Time.fixedDeltaTime * Physics.gravity.y;
        rb.velocity = rbVel;
    }

    public void setShotSpeed(Vector3 shotSpeed)
    {
        this.shotSpeed = shotSpeed;
    }

    public void setGravity(float gravity)
    {
        this.gravity = gravity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (layerMask.value != 9) Destroy(gameObject);
    }
}
