using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsOnPlayerEnter : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnElements;
    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))

                foreach (GameObject go in spawnElements)
                {
                    go.SetActive(true);
                }

                isActive = true;
    }
}
