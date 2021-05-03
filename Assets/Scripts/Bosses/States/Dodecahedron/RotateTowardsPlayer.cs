using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : State
{
    public State nextState;
    private float timer = 0;
    public override State tick(float delta)
    {
        rotate(delta);
        translate(delta);
        timer += delta;
        if (timer >= ((Dodecaethron)stateMachine).timeMoving)
        {
            timer = 0;
            var aux = ((Dodecaethron)stateMachine).GyroX.transform.localEulerAngles;
            aux.x = 63.4349488f;
            ((Dodecaethron)stateMachine).GyroX.transform.localEulerAngles = aux; //Setting GX to desired Rot

            var auxData = ((Dodecaethron)stateMachine).GyroZ.transform.rotation; //Saving GZ world Rot
            ((Dodecaethron)stateMachine).GyroX.transform.localRotation = Quaternion.identity; //Setting GX rot to 0
            ((Dodecaethron)stateMachine).GyroZ.transform.rotation = auxData; //Reseting GX's world Rot

            go.transform.position = ((Dodecaethron)stateMachine).targetPos;
            return nextState;
        }
        return this;
    }

    private void rotate(float delta)
    {
        Vector3 aux = ((Dodecaethron)stateMachine).GyroX.transform.localEulerAngles;
        aux.x += delta * 63.4349488f / ((Dodecaethron)stateMachine).timeMoving;
        ((Dodecaethron)stateMachine).GyroX.transform.localEulerAngles = aux;
    }

    private void translate(float delta)
    {
        go.transform.position = Vector3.MoveTowards(go.transform.position, ((Dodecaethron)stateMachine).targetPos, delta * ((Dodecaethron)stateMachine).distanceToMove / ((Dodecaethron)stateMachine).timeMoving);
    }
}
