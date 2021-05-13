using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateToLevel : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
