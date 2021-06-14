using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsMasked : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().material.renderQueue = 3002; // It fucking works
    }
}
