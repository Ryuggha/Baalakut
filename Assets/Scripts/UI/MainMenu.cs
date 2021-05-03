using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void CubeBotton()
    {
        SceneManager.LoadScene(1);
    }
    public void ShadowBotton()
    {
        SceneManager.LoadScene(2);
    }
    public void DodecahedrionBotton()
    {
        SceneManager.LoadScene(3);
    }
    public void TotemBotton()
    {
        SceneManager.LoadScene(4);
    }
    public void EyeBotton()
    {
        SceneManager.LoadScene(5);
    }
    public void QuitBotton()
    {
        Application.Quit();
    }
}
