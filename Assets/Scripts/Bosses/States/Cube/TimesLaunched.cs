using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesLaunched : State
{
    private int nTimesToExplote;
    private int nTimesToThorns;
    [HideInInspector] public int explosionCounter = 0;
    [HideInInspector] public int thornCounter = 0;
    public State resetState;
    public State explosionState;
    public State thornAttackState;

    private void Start()
    {
        nTimesToExplote = ((Cube)stateMachine).nTimesToExplote;
        nTimesToThorns = ((Cube)stateMachine).nTimesToThornAttack;
    }

    public override State tick(float delta)
    {
        explosionCounter++;
        thornCounter++;
        if (explosionCounter >= nTimesToExplote)
        {
            explosionCounter = 0;
            return explosionState.tick(delta);
        }
        if (thornCounter >= nTimesToThorns)
        {
            thornCounter = 0;
            return thornAttackState.tick(delta);
        }
        return resetState.tick(delta);
    }
}
