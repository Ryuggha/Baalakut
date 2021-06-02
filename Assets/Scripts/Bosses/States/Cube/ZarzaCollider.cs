using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZarzaCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            GetComponentInParent<Cube>().stunned();
        }
    }
}
