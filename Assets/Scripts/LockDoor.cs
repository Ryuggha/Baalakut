using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject toDestroy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) Destroy(toDestroy);
    }
}
