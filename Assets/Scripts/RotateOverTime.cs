using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    public Vector3 rotationDirection;
    public float durationTime;
    private float smooth;
 
    // Use this for initialization
    void Start () {
   
    }
 
    // Update is called once per frame
    void Update () {
        smooth = Time.deltaTime * durationTime;
        transform.Rotate(rotationDirection * smooth);
    }
}

