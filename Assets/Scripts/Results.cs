using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
