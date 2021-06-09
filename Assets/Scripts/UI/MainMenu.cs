using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button button;

    private void OnEnable()
    {
        button.Select();
    }

    public void PlayButton()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonIn", Vector3.zero);
        FindObjectOfType<LevelLoader>().loadLevel(1);
    }
    public void OptionsButton()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonIn", Vector3.zero);
    }
    public void QuitBotton()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonOut", Vector3.zero);
        Application.Quit();
    }

    public void Hover()
    {
        SoundHandler.playSound("event:/SFX/Menu/HoverButton", Vector3.zero);
    }
}
