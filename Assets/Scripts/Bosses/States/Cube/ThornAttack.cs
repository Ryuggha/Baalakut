using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : State
{
    public State nextState;
    public GameObject Thorn;
    private float durationOfAttack;
    private float periodOfThornAttackSpawn;

    private float globalTimer;
    private float periodTimer;

    private void Start()
    {
        durationOfAttack = ((Cube)stateMachine).durationOfAttack;
        periodOfThornAttackSpawn = ((Cube)stateMachine).periodOfThornAttackSpawn;
        globalTimer = durationOfAttack;
    }

    public override State tick(float delta)
    {
        globalTimer -= delta;
        periodTimer -= delta;

        if (periodTimer <= 0)
        {
            periodTimer = periodOfThornAttackSpawn;
            Instantiate(Thorn, stateMachine.player.gameObject.transform.position, Quaternion.identity);
        }

        if (globalTimer <= 0) 
        {
            globalTimer = durationOfAttack;
            return nextState.tick(delta);
        }

        return this;
    }
}
