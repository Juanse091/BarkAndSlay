using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenuCanvas;
    public GameObject optionsMenuCanvas;

    public void LoadGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void GoToOptions()
    {
        if (mainMenuCanvas != null && optionsMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
            optionsMenuCanvas.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        if (mainMenuCanvas != null && optionsMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
            optionsMenuCanvas.SetActive(false);
        }
    }
}
