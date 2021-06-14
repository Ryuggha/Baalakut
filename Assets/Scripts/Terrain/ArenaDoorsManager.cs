using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorsManager : MonoBehaviour
{
    private bool combatOver = false;

    public GameObject portalLock;


    private Animator animator;

    public string openAnimation, closeAnimation;

    private void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!combatOver)

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))

                animator.Play(openAnimation);
    }

    public void endCombat()
    {
        animator.Play(closeAnimation);

        if(portalLock) portalLock.SetActive(false);
        combatOver = true;
    }
}
