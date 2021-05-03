using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosSphere : MonoBehaviour
{
    public float radius = 0.3f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
