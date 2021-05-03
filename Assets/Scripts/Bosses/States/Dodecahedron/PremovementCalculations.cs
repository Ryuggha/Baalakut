using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremovementCalculations : State
{
    private bool degreeOffsetted;
    public override State tick(float delta)
    {
        float angleToTarget = Vector3.Angle(((Dodecaethron)stateMachine).GyroX.transform.forward, stateMachine.player.transform.position - go.transform.position);
        Vector3 crossToTarget = Vector3.Cross(((Dodecaethron)stateMachine).GyroX.transform.forward, stateMachine.player.transform.position - go.transform.position);
        float angleToMove;
        bool optimal = ((Dodecaethron)stateMachine).optimalMovementProbability > Random.Range(0f, 1f);

        #region ComputeAngle
        if (degreeOffsetted)
        {
            if (angleToTarget < 72)
            {
                if (crossToTarget.y > 0)
                {
                    if (optimal) angleToMove = 36;
                    else
                    {
                        if (angleToTarget > 36) angleToMove = 108;
                        else angleToMove = 324;
                    }
                }
                else
                {
                    if (optimal) angleToMove = 324;
                    else
                    {
                        if (angleToTarget > 36) angleToMove = 252;
                        else angleToMove = 36;
                    }
                }
            }
            else if (angleToTarget < 144)
            {
                if (crossToTarget.y > 0)
                {
                    if (optimal) angleToMove = 108;
                    else
                    {
                        if (angleToTarget > 108) angleToMove = 180;
                        else angleToMove = 36;
                    }
                }
                else
                {
                    if (optimal) angleToMove = 252;
                    else
                    {
                        if (angleToTarget > 108) angleToMove = 180;
                        else angleToMove = 324;
                    }
                }
            }
            else
            {
                if (optimal) angleToMove = 180;
                else
                {
                    if (crossToTarget.y > 0) angleToMove = 108;
                    else angleToMove = 252;
                }
            }
        }
        else
        {
            if (angleToTarget < 36)
            {
                if (optimal) angleToMove = 0;
                else
                {
                    if (crossToTarget.y > 0) angleToMove = 72;
                    else angleToMove = 288;
                }
            }
            else if (angleToTarget < 108)
            {
                if (crossToTarget.y > 0)
                {
                    if (optimal) angleToMove = 72;
                    else
                    {
                        if (angleToTarget > 72) angleToMove = 144;
                        else angleToMove = 0;
                    }
                }
                else
                {
                    if (optimal) angleToMove = 288;
                    else
                    {
                        if (angleToTarget > 72) angleToMove = 216;
                        else angleToMove = 0;
                    }
                }
            }
            else
            {
                if (crossToTarget.y > 0)
                {
                    if (optimal) angleToMove = 144;
                    else
                    {
                        if (angleToTarget > 144) angleToMove = 72;
                        else angleToMove = 216;
                    }
                }
                else
                {
                    if (optimal) angleToMove = 216;
                    else
                    {
                        if (angleToTarget > 144) angleToMove = 288;
                        else angleToMove = 144;
                    }
                }
            }
        }
        degreeOffsetted = true;
        #endregion


        Quaternion rotData = ((Dodecaethron)stateMachine).GyroZ.transform.rotation;

        Vector3 aux = ((Dodecaethron)stateMachine).GyroY.transform.localEulerAngles;
        aux.y += angleToMove;
        ((Dodecaethron)stateMachine).GyroY.transform.localEulerAngles = aux;

        ((Dodecaethron)stateMachine).GyroZ.transform.rotation = rotData;


        ((Dodecaethron)stateMachine).targetPos = go.transform.position + (((Dodecaethron)stateMachine).GyroX.transform.forward * ((Dodecaethron)stateMachine).distanceToMove);

        return go.GetComponentInChildren<RotateTowardsPlayer>().tick(delta);
    }
}
