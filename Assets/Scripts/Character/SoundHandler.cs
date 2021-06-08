using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{

    public static void playSound(string path, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, position);
    }

    public void thornSound()
    {
        SoundHandler.playSound("event:/SFX/Cube/ThornAppear", transform.position);
    }
}
