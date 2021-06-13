using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private ParticleSystem[] bloodParticles;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Eye_Idle");
    }

    private void Death()
    {
        animator.Play("Eye_Death");
        foreach (ParticleSystem bloodStream in bloodParticles)
        {
            bloodStream.Play();
        }
    }

}
