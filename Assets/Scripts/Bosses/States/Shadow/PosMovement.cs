using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosMovement : State
{
   
    public override State tick(float delta)
    {
        Vector3 eyePos = go.transform.GetChild(0).position;
        Vector3 playerPos = ((Shadow)stateMachine).player.transform.position + Vector3.up * 1f;
        Ray rayCast = new Ray(eyePos, (playerPos - eyePos));
        RaycastHit hit;
        if (Physics.Raycast(rayCast, out hit, 100))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) return go.GetComponentInChildren<Aiming>().tick(delta);
        }
       
        return go.GetComponentInChildren<Wait>().tick(delta);
        
    }

}
