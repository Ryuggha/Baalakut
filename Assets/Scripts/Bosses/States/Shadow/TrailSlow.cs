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

        trail.time = timetoLive;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            GameObject obj =  (GameObject)Instantiate(trigger, transform.position, Quaternion.identity);
            StartCoroutine(destroy(obj, timetoLive));
            timeLeft = timeBetweenColliders;
        }
    }

    public IEnumerator destroy(GameObject obj, float timeToLive)
    {
        yield return new WaitForSeconds(timeToLive);
        obj.GetComponent<ParticleSystem>().Stop();
        obj.GetComponent<SlowTrigger>().stop();
        Destroy(obj, 3);
    }
}
