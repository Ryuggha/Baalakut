using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimation : StateMachineBehaviour
{
    // Start is called before the first frame update
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.Play("Movement");
    }
}
