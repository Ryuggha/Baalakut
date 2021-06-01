using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandling : MonoBehaviour
{
    private SoundHandler sh;

    private const string STEP = "event:/SFX/Character/RunStep";
    private const string SHOT = "event:/SFX/Character/ProjectileShot";

    private void Start()
    {
        sh = GetComponentInParent<SoundHandler>();
    }

    public void step()
    {
        sh.playSound(STEP);
    }

    public void shotRelease()
    {
        sh.playSound(SHOT);
    }
}
