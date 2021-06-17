using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController _instance;

    public static MusicController Instance { get { return _instance; } }

    private FMOD.Studio.EventInstance musicInstance;
    private bool playing;

    public int levelIndex;
    [FMODUnity.EventRef]
    public string song;
    [FMODUnity.EventRef]
    public string stinger;

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
            musicInstance = FMODUnity.RuntimeManager.CreateInstance(song);
        }

        if (levelIndex == 3 || levelIndex == 0 || levelIndex == 1) play();
    }

    public void play()
    {
        if (!playing) musicInstance.start();
        playing = true;
    }

    public void stop(bool beaten)
    {
        playing = false;
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        if (beaten) SoundHandler.playSound(stinger, Vector3.zero);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != levelIndex && !((level == 1 && levelIndex == 0) ||(level == 0 && levelIndex == 1)))
        {
            stop(false);
            _instance = null;
            Destroy(gameObject);
        }
        else if (level == 3)
        {
            shadowLayer(0);
        }
    }

    public void shadowLayer(int i)
    {
        musicInstance.setParameterByName("ShadowCombat", i);
    }
}
