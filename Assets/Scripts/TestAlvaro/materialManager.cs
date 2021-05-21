using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialManager : MonoBehaviour
{
    public  Material mat;
    public Vector4 vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vec = transform.position;
        mat.SetVector("_Shadow", vec);
    }
}
