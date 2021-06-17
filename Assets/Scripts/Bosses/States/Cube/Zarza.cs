using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zarza : MonoBehaviour
{
    public GameObject model;
    public float modelOffsetRandomMax;
    private float rotation;
    private bool noRotate;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Random.Range(-500, 500);
        var aux = model.transform.localPosition;
        aux.x = Random.Range(-modelOffsetRandomMax, modelOffsetRandomMax);
        aux.z = Random.Range(-modelOffsetRandomMax, modelOffsetRandomMax) / 2;
        model.transform.localPosition = aux;

        var rotationAux = model.transform.localEulerAngles;
        rotationAux.y = Random.Range(0, 360f);
        model.transform.localEulerAngles = rotationAux;
    }

    private void Update()
    {
        if (!noRotate) rotate();
    }

    private void rotate()
    {
        var rot = model.transform.localEulerAngles;
        rot.y += rotation * Time.deltaTime;
        model.transform.localEulerAngles = rot;
    }

    public void stopRotating()
    {
        noRotate = true;
    }

    public void destroy(float time)
    {
        SoundHandler.playSound("event:/SFX/Cube/ThornBreak", transform.position);
        Destroy(gameObject, Random.Range(0.1f, 0.3f) + time);
    }

    public void destroy()
    {
        this.destroy(0);
    }

    public void retract(float time)
    {
        var animator = GetComponentInParent<Animator>();
        animator.Play("Retract");
    }

    public void playSound()
    {
        SoundHandler.playSound("event:/SFX/Cube/ThornAppear", transform.position);
    }
}
