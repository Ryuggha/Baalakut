using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandling : MonoBehaviour
{
    private const string STEP = "event:/SFX/Character/RunStep";
    private const string WALKSTEP = "event:/SFX/Character/WalkStep";
    private InputHandler inputs;

    private void Start()
    {
        inputs = GetComponentInParent<InputHandler>();
    }

    public void step()
    {
        if (inputs.moveAmount > .4f)
            SoundHandler.playSound(STEP, transform.position);
    }

    public void walkStep()
    {
        if (inputs.moveAmount > .01f && inputs.moveAmount <= 0.4f)
            SoundHandler.playSound(WALKSTEP, transform.position);
    }

    public void chargeStep()
    {
        SoundHandler.playSound(WALKSTEP, transform.position);
    }
}
