using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : StateMachine
{
    public GameObject backDeathAnimationPrefab;
    public float launchVelocity;
    public float retractVelocity;
    public float offSetFixed;
    public float offSetPercentage;
    public float stunTime;
    public float explosionRange;
    public float distanceToDestroyThorns = 15;
    public float zarzaSpawnTimer = 0.1f;
    public int nTimesToExplote = 4;
    public int nTimesToThornAttack = 4;
    public float durationOfAttack;
    public float periodOfThornAttackSpawn;

    [Header("Modelos")]
    public GameObject front;
    public GameObject back;

    private float distanceToMove;
    [HideInInspector] public float offSet;

    private bool stun;
    private float timer;


    public void setDistanceToMove(float distanceToMove, bool withOffsets)
    {
        offSet = distanceToMove * offSetPercentage + offSetFixed;
        if (!withOffsets) offSet = 0;
        this.distanceToMove = distanceToMove + offSet;
    }

    public float getDistanceToMove()
    {
        return this.distanceToMove;
    }

    protected override void Update()
    {
        if (stun)
        {
            if (timer == 0) timer = stunTime;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                stun = false;
            }
        }
        else base.Update();
    }

    public override void hit()
    {
        var aux = FindObjectsOfType<Zarza>();
        foreach (Zarza zarza in aux) zarza.retract(4);
        SoundHandler.playSound("event:/SFX/Cube/CubeDeath", front.transform.position);
        GetComponentInChildren<Launch>().stopSound();
        GetComponentInChildren<Retract>().stopSound();
        base.hit();
        Invoke("instantiateAfterHit", 2.9f);
    }

    public void stunned()
    {
        GetComponentInChildren<Launch>().stopSound();
        this.stun = true;
        actualState = GetComponentInChildren<PreRetract>();
        var aux = GetComponentInChildren<TimesLaunched>();
        aux.explosionCounter = nTimesToExplote;
    }

    public override void deathAnimation()
    {
        Debug.Log("asdasd");
        Instantiate(deathAnimationPrefab, front.transform.position, front.transform.rotation);
        Instantiate(backDeathAnimationPrefab, back.transform.position, back.transform.rotation);
    }
}
