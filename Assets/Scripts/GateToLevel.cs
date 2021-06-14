using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateToLevel : MonoBehaviour
{
    public int sceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameData gd = SaveSystem.LoadGame();
            gd.level = sceneIndex;
            if(SaveSystem.saveGame(gd)) FindObjectOfType<LevelLoader>().loadLevel(sceneIndex);
        }
    }
}
