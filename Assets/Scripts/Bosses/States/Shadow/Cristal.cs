using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public float timeActive;
    private float timeLeft = 0;

    public List<GameObject> trajectoryPoints;
    public int startingPoint;
    private int actualPoint = 0;
    public float vel = 1f;

    private Shadow shadow;

    private bool active = false;

    private void Start()
    {
        shadow = FindObjectOfType<Shadow>();
        actualPoint = startingPoint;
    }

    void Update()
    {
        float delta = Time.deltaTime;
        activeTick(delta);
        move(delta);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("Activated");
            timeLeft = timeActive;
            active = true;
            shadow.addActiveCrystals(1);
        }
    }

    private void activeTick(float delta)
    {
        if (active)
        {
            timeLeft -= delta;
            if (timeLeft <= 0)
            {
                active = false;
                shadow.addActiveCrystals(-1);
            }
        }
    }

    private void move(float delta)
    {
        transform.position = Vector3.MoveTowards(transform.position, trajectoryPoints[actualPoint].transform.position, vel * delta);

        if (Vector3.Distance(transform.position, trajectoryPoints[actualPoint].transform.position) < 0.3f)
        {
            actualPoint++;
            if (actualPoint >= trajectoryPoints.Count) actualPoint = 0;
        }
    }
}
