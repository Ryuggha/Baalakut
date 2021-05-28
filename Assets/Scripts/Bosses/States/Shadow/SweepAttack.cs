using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepAttack : State
{
    public GameObject slowTrigger;
    public GameObject deathTrigger;
    private float minLastingTime;
    private float maxLastingTime;
    private bool shooting;
    private float sweepAcceleration;
    private float actualAcceleration;
    private float angleToShot;
    private float actualAngle;
    private float offsetAngle;
    private float actualVelocity;
    private bool positiveAcceleration;
    private Transform eye;
    private LineRenderer ray;
    public LayerMask layerMask;
    private Color color;

    private void Start()
    {
        color = ((Shadow)stateMachine).shotColor;
        minLastingTime = ((Shadow)stateMachine).minDarknessLastingTime;
        maxLastingTime = ((Shadow)stateMachine).maxDarknessLastingTime;

        eye = go.transform.GetChild(0);
        ray = eye.gameObject.GetComponent<LineRenderer>();
    
        angleToShot = ((Shadow)stateMachine).angleToShot;
        sweepAcceleration = ((Shadow)stateMachine).sweepAcceleration;
    }

    public override State tick(float delta)
    {
        if (!shooting)
        {
            ray.startColor = color;
            ray.endColor = color;
            actualVelocity = 0;
            ray.startWidth = ((Shadow)stateMachine).ShotRayWidth;
            ray.endWidth = ((Shadow)stateMachine).ShotRayWidth;
            shooting = true;
            offsetAngle = Vector3.Angle(go.transform.forward, stateMachine.player.transform.position - go.transform.position);
            var cross = Vector3.Cross(go.transform.forward, stateMachine.player.transform.position - go.transform.position);
            if (cross.y < 0) offsetAngle = -offsetAngle;

            decideDirection();
            if (positiveAcceleration) actualAcceleration = sweepAcceleration;
            else actualAcceleration = -sweepAcceleration;

            if (positiveAcceleration) actualAngle = -angleToShot;
            else actualAngle = angleToShot;

            ray.SetPosition(0, eye.transform.position);
            ray.enabled = true;
        }

        actualAngle += actualVelocity * delta;

        if (Mathf.Abs(actualAngle) > angleToShot)
        {
            ray.enabled = false;
            shooting = false;
            return stateMachine.GetComponentInChildren<FastMovement>();
        }

        
        Vector3 direction = eye.rotation * new Vector3(Mathf.Sin(Mathf.Deg2Rad * (actualAngle + offsetAngle)), 0, Mathf.Cos(Mathf.Deg2Rad * (actualAngle + offsetAngle))).normalized;
        RaycastHit hit;
        Physics.Raycast(eye.transform.position, direction, out hit, 50, layerMask);
        ray.SetPosition(1, hit.point);
        actualVelocity += actualAcceleration * delta;
        actualAcceleration += actualAcceleration * delta;

        float period = Vector3.Distance(ray.GetPosition(0), ray.GetPosition(1));
        for (int i = 0; i < period / 4; i++)
        {
            GameObject obj = (GameObject)Instantiate(slowTrigger, ray.GetPosition(0) + (ray.GetPosition(1) - ray.GetPosition(0)).normalized * i*4, Quaternion.identity);
            StartCoroutine(destroy(obj, Random.Range(minLastingTime/10, maxLastingTime/10)));
        }
        for (int i = 0; i < (hit.point - eye.position).magnitude * 4; i++)
        {
            var auxPos = ray.GetPosition(0) + ((ray.GetPosition(1) - ray.GetPosition(0)).normalized * (i / 4));
            auxPos.y = 0;
            GameObject auxObj = Instantiate(deathTrigger, auxPos, Quaternion.identity);
            auxObj.GetComponent<DeathTriggerBeam>().player = stateMachine.player;
        }

        return this;
    }

    private void decideDirection()
    {
        int aux = Random.Range(0, 2);
        if (aux == 0) positiveAcceleration = true;
        else positiveAcceleration = false;
    }

    public IEnumerator destroy(GameObject obj, float timeToLive)
    {
        yield return new WaitForSeconds(timeToLive);
        obj.GetComponent<ParticleSystem>().Stop();
        obj.GetComponent<SlowTrigger>().stop();
        Destroy(obj, 3);
    }
}
