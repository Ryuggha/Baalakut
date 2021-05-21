using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceProtector : MonoBehaviour
{
    public bool weakProtector;
    public bool startsDeactivated;
    private bool isActive;
    private bool isGrounded;

    private void Start()
    {
        if (startsDeactivated) GetComponentInChildren<ProtectorActivation>().activate();
    }

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
