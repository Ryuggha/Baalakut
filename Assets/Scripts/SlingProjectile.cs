using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingProjectile : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject impactParticles;

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
        transform.forward = rb.velocity.normalized;
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
        if (layerMask.value != 9)
        {
            SoundHandler.playSound("event:/SFX/Character/ProjectileImpact", transform.position);
            Instantiate(impactParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WeakSpot ws = other.gameObject.GetComponentInParent<WeakSpot>();
        if (ws != null)
        {
            ws.hit();
        }
        else
        {
            LockDoor ld = other.gameObject.GetComponentInParent<LockDoor>();
            if (ld != null)
            {
                ld.hit();
            }
        }
    }
}
