using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int level;
    public bool shadowKilled, cubeKilled, dodecahedroKilled;
    public float[] position;
    public float[] rotation;

    public GameData()
    {
        level = 1;
        shadowKilled = false;
        cubeKilled = false;
        dodecahedroKilled = false;
    }
    public GameData(int level, bool shadowKilled, bool cubeKilled, bool dodecahedroKilled, Transform player)
    {
        this.level = level;
        this.shadowKilled = shadowKilled;
        this.cubeKilled = cubeKilled;
        this.dodecahedroKilled = dodecahedroKilled;
        position = new float[3];
        position[0] = player.position.x;
        position[1] = player.position.y;
        position[2] = player.position.z;

        rotation = new float[3];
        rotation[0] = player.transform.rotation.x;
        rotation[1] = player.transform.rotation.y;
        rotation[2] = player.transform.rotation.z;
    }   
}
