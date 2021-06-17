using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator shake (float duration, float force)
    {
        Debug.Log("Shaking during: " +duration + "with a force of " + force);
        Vector3 startingPos = transform.localPosition;
        float timer = 0f;

        while(timer < duration)
        {
            if (Time.timeScale > 0)
            {
                float x = Random.Range(-force, force);
                float y = Random.Range(-force, force);
                transform.localPosition = new Vector3(x, y, startingPos.z);

                timer += Time.deltaTime;
            }
            yield return null;
        }

        transform.localPosition = startingPos;
    }
}
