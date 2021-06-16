using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    private Animator animator;

    public bool vulnerable = true;

    [SerializeField]
    private ParticleSystem[] bloodParticles;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Eye_Idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("AlliedProjectile") && vulnerable) Death();
        Debug.Log("HEY blood");
    }
  
    public void Death()
    {
        animator.Play("Eye_Death");
        foreach (ParticleSystem bloodStream in bloodParticles)
        {
           ParticleSystem ps =  Instantiate(bloodStream, transform.position, Quaternion.identity) as ParticleSystem;
           ps.Play();
        }
    }

}
