using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator shake (float duration, float force)
    {
        Vector3 startingPos = transform.localPosition;
        float timer = 0f;

        while(timer < duration)
        {
            float x = Random.Range(-force, force);
            float y = Random.Range(-force, force);
            transform.localPosition = new Vector3(x, y, startingPos.z);

            timer += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = startingPos;
    }
}
