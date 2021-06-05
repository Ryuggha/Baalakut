using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandling : MonoBehaviour
{
    private SoundHandler sh;

    private const string STEP = "event:/SFX/Character/RunStep";

    private void Start()
    {
        sh = GetComponentInParent<SoundHandler>();
    }

    public void step()
    {
        SoundHandler.playSound(STEP, transform.position);
    }
}
