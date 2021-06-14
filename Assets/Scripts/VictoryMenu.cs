using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
   public void Atras()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonIn", Vector3.zero);
        
        if (SaveSystem.DeleteData()) FindObjectOfType<LevelLoader>().loadLevel(0);
    }
}
