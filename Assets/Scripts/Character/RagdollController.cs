using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody[] rigidbodies;
    public bool hola;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        ragdollEnabled(false);
    }

    public void ragdollEnabled(bool b)
    {
        bool isKinematic = !b;

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = isKinematic;
            if (b) rb.gameObject.layer = 9;
        }
        anim.enabled = !b;
    }

    private void Update()
    {
        if (hola) ragdollEnabled(true);
    }
}
