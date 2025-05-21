using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroController : MonoBehaviour
{
    public AudioSource logoSound;
    public CanvasGroup fadeOverlay; // Asignar desde el Inspector
    public float fadeDuration = 1.5f;

    public void PlayLogoSound()
    {
        if (logoSound != null)
            logoSound.Play();
    }

    public void LoadMainMenu()
    {
        StartCoroutine(FadeOutAndLoadScene("MainMenu"));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // Se asegura de que el overlay esté completamente opaco
        if (fadeOverlay != null)
            fadeOverlay.alpha = 1;

        // Cargar la escena
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Espera 0.3s (tiempo de respiro) antes de comenzar el fade-out
        yield return new WaitForSeconds(0.3f);

        // Empieza a hacer fade out
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            if (fadeOverlay != null)
                fadeOverlay.alpha = 1 - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        // Activar la nueva escena una vez termine el fade
        asyncLoad.allowSceneActivation = true;
    }
}
