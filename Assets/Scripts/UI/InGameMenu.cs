using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private InputHandler inputs;
    private bool active;

    public Button menuFirstButton;
    public GameObject inGameMenu;
    public GameObject optionsMenu;

    private void Start()
    {
        inputs = FindObjectOfType<InputHandler>();
    }

    public void PlayButton()
    {
        toggleActive();
    }
    public void OptionsButton()
    {

    }
    public void QuitBotton()
    {
        SceneManager.LoadScene(0);
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
    }

    public bool getPaused()
    {
        return active;
    }
}
