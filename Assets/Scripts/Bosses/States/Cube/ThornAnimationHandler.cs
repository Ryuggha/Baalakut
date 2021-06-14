using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAnimationHandler : MonoBehaviour
{
    public Zarza[] zarzas;

    private int count;

    public void trigger()
    {
        if (count < zarzas.Length)
            zarzas[count].stopRotating();
        count++;
    }
}
