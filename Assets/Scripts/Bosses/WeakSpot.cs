using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    private StateMachine sm;
    void Start()
    {
        sm = GetComponentInParent<StateMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == 7) hit();
    }

    public void hit()
    {
        sm.hit();
    }
}
