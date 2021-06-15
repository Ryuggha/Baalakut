using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State actualState;
    protected bool dead;

    [Header("Boss Eye")]
    public EyeController eye;

    [Header("Boss State Stats")]
    public State startingState;
    public GameObject deathAnimationPrefab;

    [HideInInspector] public PlayerManager player;

    [SerializeField] private ArenaDoorsManager doorsManager;


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

    public virtual void die()
    {
        if (!dead)
        {
            if(eye != null) eye.Death();
            SoundHandler.playSound("event:/SFX/Character/CriticalHit", transform.position);
            dead = true;
            doorsManager.endCombat();
            FindObjectOfType<MusicController>().stop(true);
            Invoke("destroyGO", 3);

        }        
    }

    private void destroyGO()
    {
        deathAnimation();
        Destroy(gameObject);
    }

    public virtual void deathAnimation()
    {
        GetComponentInChildren<EyeController>().death();
        if (deathAnimationPrefab != null)
            Instantiate(deathAnimationPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
