using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorActivation : MonoBehaviour
{
    private FaceProtector fp;
    // Start is called before the first frame update
    void Awake()
    {
        fp = GetComponentInParent<FaceProtector>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            activate();
        }
    }

    public void activate()
    {
        fp.setActiveFace();
        Destroy(gameObject);
    }
}
