using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDode : MonoBehaviour
{
    GameData gameData;
    public GameObject Dode, DoorManager;

    // Start is called before the first frame update
    void Start()
    {
        gameData = SaveSystem.LoadGame();
        if (gameData == null) gameData = new GameData();

        if (gameData.dodecahedroKilled)
        {
            Dode.SetActive(false);
            DoorManager.GetComponent<ArenaDoorsManager>().enabled = false;
        }
    }
}
