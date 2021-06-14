using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int level;
    public bool shadowKilled, cubeKilled, dodecahedroKilled;
    public float[3] position;
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
        position[] = new float[3];
    }   
}
