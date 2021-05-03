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
        sm.die();
    }
}
