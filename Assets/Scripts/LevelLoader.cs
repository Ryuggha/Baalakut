using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    private bool loading;

    public void loadLevel(int index)
    {
        if (!loading) StartCoroutine(LoadAsaync(index));
    }

    IEnumerator LoadAsaync (int index)
    {
        if (index < 0) index = SceneManager.GetActiveScene().buildIndex;
        loading = true;
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(0.7f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
