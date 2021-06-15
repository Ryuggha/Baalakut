using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorsManager : MonoBehaviour
{
    private bool combatOver = false;

    public GameObject portalLock;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject boss;
    [SerializeField] private bool startsActive;

    public string openAnimation, closeAnimation;

    private void OnTriggerEnter(Collider other)
    {
        if (!combatOver && boss.active == startsActive)

            if (other.TryGetComponent(out PlayerManager pm))

                animator.Play(openAnimation);
    }

    public void endCombat()
    {
        animator.Play(closeAnimation);

        if(portalLock) portalLock.SetActive(false);
        combatOver = true;
    }
}
