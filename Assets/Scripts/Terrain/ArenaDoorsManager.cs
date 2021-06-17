using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorsManager : MonoBehaviour
{
    private bool combatOver = false;
    private bool combatInProgres = false;

    public GameObject portalLock;

    [FMODUnity.EventRef]
    public string doorsSound;
    public Transform soundTransform;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject boss;
    [SerializeField] private bool startsActive;


    public string openAnimation, closeAnimation;

    private void Awake()
    {
        playSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!combatInProgres&&!combatOver && boss.active == startsActive)

            if (other.TryGetComponent(out PlayerManager pm))
            {
                animator.Play(openAnimation);
                combatInProgres = true;
                playSound();
            }
    }

    public void endCombat()
    {
        animator.Play(closeAnimation);
        playSound();
        if(portalLock) portalLock.SetActive(false);
        combatOver = true;
    }

    private void playSound()
    {
        SoundHandler.playSound(doorsSound, soundTransform.position);
    }

}
