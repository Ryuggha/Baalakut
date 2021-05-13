using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zarza : MonoBehaviour
{
    public GameObject model;
    public float modelOffsetRandomMax;

    // Start is called before the first frame update
    void Start()
    {
        var aux = model.transform.localPosition;
        aux.x = Random.Range(-modelOffsetRandomMax, modelOffsetRandomMax);
        aux.z = Random.Range(-modelOffsetRandomMax, modelOffsetRandomMax) / 2;
        model.transform.localPosition = aux;
    }

    public void destroy()
    {
        Destroy(gameObject, Random.Range(0.1f, 0.3f));
    }
}
