using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorManager : MonoBehaviour
{
    [Header("References")]
    public GameObject shadow_Eye, dodecahedro_Eye, cube_Eye;
    public bool isActive;
    [SerializeField] public Material inactiveEyeMat, activeEyeMat;
    [SerializeField] public MeshRenderer[] shadowEyeMesh, dodecEyeMesh, cubeEyeMesh;

    [Header("Open Portal Stats")]
    [SerializeField]
    private float speed = 0.2f;
    [SerializeField]
    private float distanceDown = 3;
    [SerializeField]
    private float angularSpeed = 3;

    private static int shadowId, dodecahedroId, cubeId;
    private bool shadow, dodecahedro, cube;
    private int i = 0;
    private bool open = false;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        shadowId = shadow_Eye.GetInstanceID();
        dodecahedroId = dodecahedro_Eye.GetInstanceID();
        cubeId = cube_Eye.GetInstanceID();
        target = transform.position + Vector3.down * distanceDown;
        GameData gameData = SaveSystem.LoadGame();

        bool shadowKilled = gameData.shadowKilled;
        bool cubeKilled = gameData.cubeKilled;
        bool dodecahedroKilled = gameData.dodecahedroKilled;

        shadow_Eye.SetActive(shadowKilled);
        cube_Eye.SetActive(cubeKilled);
        dodecahedro_Eye.SetActive(dodecahedroKilled);

        isActive = shadowKilled && cubeKilled && dodecahedroKilled;

        if(isActive){

            foreach (var mesh in shadowEyeMesh)
            {   
                if(mesh != null)
                    mesh.material = activeEyeMat;
            }

            foreach (var mesh in dodecEyeMesh)
            {   
                if(mesh != null)
                    mesh.material = activeEyeMat;
            }

            foreach (var mesh in cubeEyeMesh)
            {   
                if(mesh != null)
                    mesh.material = activeEyeMat;
            }

        } else {
            foreach (var mesh in shadowEyeMesh)
            {   
                if(mesh != null)
                    mesh.material = inactiveEyeMat;
            }

            foreach (var mesh in dodecEyeMesh)
            {   
                if(mesh != null)
                    mesh.material = inactiveEyeMat;
            }

            foreach (var mesh in cubeEyeMesh)
            {   
                if(mesh != null)
                    mesh.material = inactiveEyeMat;
            }

        }

    }

    public void hitEye(GameObject eye)
    {
        
        if (isActive)
        {
            SoundHandler.playSound("event:/SFX/Character/CriticalHit", eye.transform.position);
            if (eye.GetInstanceID() == shadowId && !shadow) i++;
            else if(eye.GetInstanceID() ==cubeId && !cube) i++;
            else if (eye.GetInstanceID() == dodecahedroId && !dodecahedro) i++;
            eye.GetComponent<EyeController>().Death();
            Destroy(eye);
            
            if (i == 3) openPortal();
        }
        
    }



    private void openPortal()
    {
        open = true;
    }

    private void Update()
    {
        if (open)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime);
        }
        
    }
}
