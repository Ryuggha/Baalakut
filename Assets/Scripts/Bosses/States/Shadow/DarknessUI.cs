using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarknessUI : MonoBehaviour
{
    public Image image;

    public void setImageAlpha(float alpha)
    {
        Color aux = image.color;
        aux.a = alpha;
        image.color = aux;
    }
}
