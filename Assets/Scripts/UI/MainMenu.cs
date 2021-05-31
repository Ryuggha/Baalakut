using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene(1);
    }
    public void OptionsButton()
    {

    }
    public void QuitBotton()
    {
        Application.Quit();
    }
}
