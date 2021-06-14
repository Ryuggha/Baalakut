using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShadow : MonoBehaviour
{
    GameData gameData;
    public GameObject Shadow, DoorManager;

    // Start is called before the first frame update
    void Start()
    {
        gameData = SaveSystem.LoadGame();
        if (gameData == null) gameData = new GameData();

        if (gameData.shadowKilled)
        {
            Shadow.SetActive(false);
            DoorManager.GetComponent<ArenaDoorsManager>().enabled = false;
        }
    }
}
