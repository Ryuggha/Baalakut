using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera cam;

    public bool staticBillBoard;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!staticBillBoard)
        {
            transform.LookAt(cam.transform);
        }
        else
        {
            transform.rotation = cam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

    }
}
