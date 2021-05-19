using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorsManager : MonoBehaviour
{
    
    private bool combatOver = false;

    public GameObject[] Doors;


    private void OnTriggerEnter(Collider other)
    {
        if (!combatOver)

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))

                foreach (GameObject go in Doors)

                    go.SetActive(!go.active);
    }

    public void endCombat()
    {
        foreach (GameObject go in Doors)

                go.SetActive(!go.active);
        
        combatOver = true;
    }
}
