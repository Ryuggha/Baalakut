using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State actualState;
    private bool dead;

    [Header("Boss State Stats")]
    public State startingState;

    [HideInInspector] public PlayerManager player;


    void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        actualState = startingState;
        var stateList = GetComponentsInChildren<State>();
        foreach (State state in stateList)
        {
            state.go = gameObject;
            state.stateMachine = this;
        }
    }

    protected virtual void Update()
    {
        float delta = Time.deltaTime;
        if (!dead)
            actualState = actualState.tick(delta);
    }

    public virtual void hit()
    {
        die();
    }

    public void die()
    {
        Debug.Log("Ded");
        dead = true;
        Destroy(gameObject, 3);
    }
}
