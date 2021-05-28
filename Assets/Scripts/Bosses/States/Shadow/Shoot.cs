using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : State
{
    private Transform eye;
    private LineRenderer ray;

    private float timeShot = 0f;

    private bool shootting = false;

    [Header("DarkenssTrail")]
    private float minLastingTime;
    private float maxLastingTime;
    public GameObject trigger;
    private Color color;

    private void Start()
    {
        color = ((Shadow)stateMachine).shotColor;
        eye = go.transform.GetChild(0);
        ray = eye.gameObject.GetComponent<LineRenderer>();
        minLastingTime = ((Shadow)stateMachine).minDarknessLastingTime;
        maxLastingTime = ((Shadow)stateMachine).maxDarknessLastingTime;
    }

    public override State tick(float delta)
    {
        RaycastHit hit;
        Ray rayCast =new Ray();

        if (!shootting)
        {
            ray.startColor = color;
            ray.endColor = color;
            shootting = true;
            timeShot = 0f;
            eye.gameObject.GetComponent<LineRenderer>().enabled = true;
            ray.startWidth = ((Shadow)stateMachine).ShotRayWidth;
            ray.endWidth = ((Shadow)stateMachine).ShotRayWidth;

            rayCast = new Ray(ray.GetPosition(0), ray.GetPosition(1)-ray.GetPosition(0));

            float distance = Vector3.Distance(ray.GetPosition(0), ray.GetPosition(1));
            for (int i = 0; i < distance/1; i++)
            {
                GameObject obj = Instantiate(trigger, ray.GetPosition(0) + (ray.GetPosition(1) - ray.GetPosition(0)).normalized * i, Quaternion.identity);
                StartCoroutine(destroy(obj, Random.Range(minLastingTime, maxLastingTime)));
            }

            if (Physics.Raycast(rayCast, out hit, distance, LayerMask.GetMask("Player")))
            {
                ((Shadow)stateMachine).player.GetComponentInChildren<PlayerManager>().takeDamage();
            }
        }

        timeShot += delta;
       
        if (timeShot >= ((Shadow)stateMachine).ShotDuration)
        {
            eye.gameObject.GetComponent<LineRenderer>().enabled = false;
            shootting = false;
            return go.GetComponentInChildren<PreMovement>();
        }
        else return this;
    }

    public void setShootting(bool var)
    {
        shootting = var;
    }

    public IEnumerator destroy(GameObject obj, float timeToLive)
    {
        yield return new WaitForSeconds(timeToLive);
        obj.GetComponent<ParticleSystem>().Stop();
        obj.GetComponent<SlowTrigger>().stop();
        Destroy(obj, 3);
    }
}
