using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingController : MonoBehaviour
{
    public float waitTime = 3f; // segundos de espera
    public string sceneToLoad = "SampleScene";

    void Start()
    {
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
