using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    private Transform cam;
    private Color color;
    private Image image;
    public float clearViewDistance;
    public float farViewDistance;

    private void Start()
    {
        cam = GetComponent<Canvas>().worldCamera.transform;
        image = GetComponentInChildren<Image>();
        color = image.color;
    }

    private void LateUpdate()
    {
        color.a = (-(Vector3.Distance(transform.position, cam.position) - farViewDistance)) / (farViewDistance - clearViewDistance);
        image.color = color;
        transform.LookAt(transform.position + cam.forward);
    }
}
