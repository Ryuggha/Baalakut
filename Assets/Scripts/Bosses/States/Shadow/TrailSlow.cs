using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSlow : MonoBehaviour
{
    
    public Object trigger;
    private float timeBetweenColliders = 0.5f;
    private float timeLeft = 0;
    private float timetoLive;
    private float slowPower;
    // Start is called before the first frame update
    void Start()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();
        
        timetoLive = GetComponentInParent<Shadow>().TrailDuration;
        slowPower = GetComponentInParent<Shadow>().TrailSlowPower;

        trail.time = timetoLive;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            GameObject obj =  (GameObject)Instantiate(trigger, transform.position, Quaternion.identity);
            obj.GetComponent<SlowTrigger>().setSlowPower(slowPower);
            Destroy(obj, timetoLive);
            timeLeft = timeBetweenColliders;
        }
    }
}
