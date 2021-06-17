using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField]
    private Transform body;
    [SerializeField]
    private Transform legRoot;
    [SerializeField]
    private float stepDistance = 0.5f;
    [SerializeField]
    private float footSpacing = 13;
    [SerializeField]
    private float stepHeight;

    [SerializeField]
    public float speed = 1;
    private float lerp = 1;
    public LayerMask layer;

    public bool legGrounded;
    public IKFootSolver otherLegReference;

    private Vector3 legDirection;
    private Vector3 currentPosition;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private float distance;
    private bool diying = false;
    private Transform diyingPos;
    private bool setUp = true;


    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
        oldPosition = transform.position;
        legDirection = body.InverseTransformDirection( Vector3.Normalize(currentPosition - body.position));
        legDirection.y = 0;
    }


    public void Die(Transform pos)
    {
        diying = true;
        diyingPos = pos;
        Debug.Log("FOOT DIYING");
    }

    // Update is called once per frame
    void Update()
    {

       
        transform.position = currentPosition;

        Ray ray = new Ray(legRoot.position + (body.TransformDirection(legDirection) * footSpacing), Vector3.down);
        if (diying)
        {

            if (setUp)
            {
                lerp = 0;
                legGrounded = false;
                setUp = false;
                
            }

            newPosition = diyingPos.position;
            distance = Vector3.Distance(oldPosition, newPosition);

            lerp += Time.deltaTime * speed;
                Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
              

                currentPosition = footPosition;
                
            

            
        }
        else if (Physics.Raycast(ray, out RaycastHit info, 10,layer)){

            if (Vector3.Distance(newPosition, info.point) >= stepDistance && otherLegReference.legGrounded) 
                {
                
                    lerp = 0;
                    newPosition = info.point;
                    legGrounded = false;

                    distance = Vector3.Distance(oldPosition, newPosition);

                    //transform.position = info.point;
                    //currentPosition = transform.position;
                }

            if (lerp < 1)
                {
                    Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
                    footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

                    currentPosition = footPosition;
                    lerp += Time.deltaTime * speed * distance;
                }

            else
                {
                    oldPosition = newPosition;
                    legGrounded = true;
                }

            

            
           
        }
    }

  
}
