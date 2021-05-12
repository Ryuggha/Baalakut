using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorGroundDetector : MonoBehaviour
{
    private FaceProtector fp;

    // Start is called before the first frame update
    void Start()
    {
        fp = GetComponentInParent<FaceProtector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12) fp.isTouchingGround(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12) fp.isTouchingGround(false);
    }
}
