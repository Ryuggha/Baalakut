using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceProtector : MonoBehaviour
{
    public bool weakProtector;
    private bool isActive;
    private bool isGrounded;

    public void setActiveFace() 
    {
        isActive = true;
        if (weakProtector) GetComponentInParent<Dodecaethron>().setWeakState();
    }

    public void isTouchingGround(bool b)
    {
        if (isActive)
        {
            if (b) isGrounded = true;
            else isGrounded = false;
        }
    }

    public bool getIsGrounded()
    {
        return this.isGrounded;
    }
}
