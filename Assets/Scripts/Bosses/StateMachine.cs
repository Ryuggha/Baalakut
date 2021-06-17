using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State actualState;
    protected bool dead;

    
    private EyeController eye;

    [Header("Boss State Stats")]
    public State startingState;
    public GameObject deathAnimationPrefab;
    public bool KillBoss = false;
    

    [HideInInspector] public PlayerManager player;

    [SerializeField] private ArenaDoorsManager doorsManager;


    void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        actualState = startingState;
        var stateList = GetComponentsInChildren<State>();
        eye = GetComponentInChildren<EyeController>();
        foreach (State state in stateList)
        {
            state.go = gameObject;
            state.stateMachine = this;
        }
    }

    protected virtual void Update()
    {
        float delta = Time.deltaTime;
        if (KillBoss && !dead) die();
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
            player.makePlayerInvincible();
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
        if (deathAnimationPrefab != null) Instantiate(deathAnimationPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
