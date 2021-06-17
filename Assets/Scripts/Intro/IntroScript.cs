using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public CameraShake camera;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shake()
    {
        SoundHandler.playSound("event:/SFX/Dodecahedron/DodecahedronDoors", transform.position);
        StartCoroutine(camera.shake(0.5f, 0.1f));
    }

    private void EndIntro()
    {
        LevelLoader ll = FindObjectOfType<LevelLoader>();

        ll.loadLevel(1);
    }
}
