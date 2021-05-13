using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrigger : MonoBehaviour
{
    private float slowPower = 1;
    public void setSlowPower(float var)
    {
        slowPower = var;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponentInChildren<Movement>().setSpeedModifier(slowPower);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponentInChildren<Movement>().setSpeedModifier(1f);
        }
    }
}
