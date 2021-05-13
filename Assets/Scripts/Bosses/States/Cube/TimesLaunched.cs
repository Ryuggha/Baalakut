using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesLaunched : State
{
    private int nTimesToExplote;
    [HideInInspector] public int counter = 0;
    public State resetState;
    public State explosionState;

    private void Start()
    {
        nTimesToExplote = ((Cube)stateMachine).nTimesToExplote;
    }

    public override State tick(float delta)
    {
        counter++;
        if (counter >= nTimesToExplote)
        {
            counter = 0;
            return explosionState.tick(delta);
        }
        return resetState.tick(delta);
    }
}
