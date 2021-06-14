using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHub : MonoBehaviour
{
    GameData gameData;
    public GameObject shadowDoor, cubeDoor, dodeDoor;

    // Start is called before the first frame update
    void Start()
    {
        gameData = SaveSystem.LoadGame();
        if (gameData == null) gameData = new GameData();

        if (gameData.shadowKilled) shadowDoor.SetActive(false);
           
        if (gameData.cubeKilled) cubeDoor.SetActive(false);

        if (gameData.dodecahedroKilled) dodeDoor.SetActive(false);
    }

    
}
