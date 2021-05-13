using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    private PlayerManager playerManager;
    public Animator anim;
    private InputHandler inputHandler;
    private Movement playerMovement;
    private int vertical, horizontal;
    public bool canRotate;

    public void Initialize()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        anim = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
        playerMovement = GetComponentInParent<Movement>();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("IsInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        anim.SetFloat(vertical, Mathf.Clamp(verticalMovement, -1, 1), 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, Mathf.Clamp(horizontalMovement, -1, 1), 0.1f, Time.deltaTime);
    }

    public void setCharging(bool isCharging)
    {
        if (isCharging) anim.Play("Charge");
        else anim.Play("Shot");
        anim.SetBool("Charging", isCharging);
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }

    private void OnAnimatorMove()
    {
        if (!playerManager.isInteracting) return;
        playerMovement.rigidbody.drag = 0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / Time.deltaTime;
        playerMovement.rigidbody.velocity = velocity;
    }
}
