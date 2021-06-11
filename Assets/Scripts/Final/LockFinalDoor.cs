using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockFinalDoor : MonoBehaviour
{
    public FinalDoorManager doorManager;
    
    public void hit()
    {
      
        doorManager.hitEye(this.gameObject);
    }
}
