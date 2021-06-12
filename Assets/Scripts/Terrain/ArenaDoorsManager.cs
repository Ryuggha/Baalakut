using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorsManager : MonoBehaviour
{
    
    private bool combatOver = false;

    public GameObject[] Doors;
    public GameObject portalLock;

    public bool startsActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!combatOver)

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))

                foreach (GameObject go in Doors)

                    go.SetActive(!startsActive);
    }

    public void endCombat()
    {
        foreach (GameObject go in Doors)

                go.SetActive(startsActive);

        if(portalLock) portalLock.SetActive(false);
        combatOver = true;
    }
}
