using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameData gd = SaveSystem.LoadGame();
            Transform player = transform;    //Set player position and rotation data with checkpoint transform
            gd.setPositionAndRotation(player);

            SaveSystem.saveGame(gd);
        }
    }
}
