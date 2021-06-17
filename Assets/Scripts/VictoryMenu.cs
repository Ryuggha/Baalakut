using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    public Button atras;
    private void Start()
    {
        atras.Select();
    }
    public void Atras()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonIn", Vector3.zero);
        Debug.Log("reset this");
        if (SaveSystem.DeleteData()) ;
        FindObjectOfType<LevelLoader>().loadLevel(0);
    }

    public void Hover()
    {
        SoundHandler.playSound("event:/SFX/Menu/HoverButton", Vector3.zero);
    }
}
