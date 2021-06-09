using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    private InputHandler inputs;
    private bool active;
    private bool dead;
    private float deathTime;
    private float timer;

    public float textDeathTimer = 1;

    public Button menuFirstButton;
    public GameObject inGameMenu;
    public GameObject optionsMenu;
    public Image deathPanel;
    public Image deathText;

    private void Start()
    {
        inputs = FindObjectOfType<InputHandler>();
        deathTime = FindObjectOfType<PlayerManager>().deathTimer;
    }

    public void PlayButton()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonIn", Vector3.zero);
        toggleActive();
    }
    public void OptionsButton()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonIn", Vector3.zero);
    }
    public void QuitBotton()
    {
        SoundHandler.playSound("event:/SFX/Menu/ClickButtonOut", Vector3.zero);
        toggleActive();
        FindObjectOfType<LevelLoader>().loadLevel(0);
    }

    public void toggleActive()
    {
        if (active)
        {
            Time.timeScale = 1;
            inGameMenu.SetActive(false);
        }
        else
        {
            menuFirstButton.Select();
            inGameMenu.SetActive(true);
            Time.timeScale = 0;
        }
        active = !active;
    }

    private void LateUpdate()
    {
        if (inputs.menuFlag) toggleActive();
        if (dead)
        {
            timer += Time.deltaTime;
            Color color = deathPanel.color;
            color.a = Mathf.Clamp01(timer / deathTime);
            deathPanel.color = color;

            color = deathText.color;
            color.a = Mathf.Clamp01(timer);
            deathText.color = color;
        }
    }

    public bool getPaused()
    {
        return active;
    }
    public void die()
    {
        dead = true;
        timer = 0;
    }

    public void hover()
    {
        SoundHandler.playSound("event:/SFX/Menu/HoverButton", Vector3.zero);
    }

}
