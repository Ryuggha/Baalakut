using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCube : MonoBehaviour
{
    GameData gameData;
    public GameObject cube, Door;

    // Start is called before the first frame update
    void Start()
    {
        gameData = SaveSystem.LoadGame();
        if (gameData == null) gameData = new GameData();

        if (gameData.cubeKilled)
        {
            cube.SetActive(false);
            Door.SetActive(false);
        }
    }
}
