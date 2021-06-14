using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHub : MonoBehaviour
{
    GameData gameData;
    public GameObject shadowDoor, cubeDoor, dodeDoor;

    public Transform player;
    

    // Start is called before the first frame update
    void Start()
    {

        gameData = SaveSystem.LoadGame();
        if (gameData == null) gameData = new GameData();
        if (true)
        {
            Vector3 pos = new Vector3(gameData.position[0], gameData.position[1], gameData.position[2]);
            Quaternion rot = new Quaternion(gameData.rotation[0], gameData.rotation[1], gameData.rotation[2], gameData.rotation[3]);

            Debug.Log("Position: " + pos);
            Debug.Log("Rotation: " + rot);

            player.SetPositionAndRotation(pos, rot);

        }

        

        if (gameData.shadowKilled) shadowDoor.SetActive(false);
           
        if (gameData.cubeKilled) cubeDoor.SetActive(false);

        if (gameData.dodecahedroKilled) dodeDoor.SetActive(false);

        
    }

    private void Awake()
    {
        
    }


}
