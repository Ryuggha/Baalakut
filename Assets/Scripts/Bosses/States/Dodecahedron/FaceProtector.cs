using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceProtector : MonoBehaviour
{
    private bool isActive;
    private bool isGrounded;

    public void setActiveFace() 
    {
        isActive = true;    
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
