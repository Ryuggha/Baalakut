using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class DashDistortion : MonoBehaviour  
{   

    public UnityEngine.Rendering.PostProcessing.LensDistortion distortion;

    // public Volume volume;

    // Start is called before the first frame update
    void Start()
    {
        // distortion = GetComponent<UnityEngine.Rendering.PostProcessing.LensDistortion>();
        // distortion.intensity.Override(-100);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
