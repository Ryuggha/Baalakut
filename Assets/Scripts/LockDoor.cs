using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject toDestroy;
    private EyeController eye;

    private void Start()
    {
        eye = GetComponent<EyeController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == 7) hit();
    }
    public void hit()
    {
        SoundHandler.playSound("event:/SFX/Character/CriticalHit", transform.position);
        eye.Death();
        Destroy(toDestroy);
    }
}
