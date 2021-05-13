using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public float timeActive;
    public float timeLeft = 0;

    private BoxCollider[] triggers;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        triggers = GetComponents<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                foreach (BoxCollider trigger in triggers)   trigger.enabled = false;

                active = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(BoxCollider trigger in  triggers)
        {
            trigger.enabled = true;
            timeLeft = timeActive;
            active = true;
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Shadow shadow;
        
        if (other.gameObject.TryGetComponent<Shadow>(out shadow))
        {
            shadow.MakeItVulnerable();
            foreach (BoxCollider trigger in triggers)   trigger.enabled = false;
            
        }

    }
}
