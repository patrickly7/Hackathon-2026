using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    public void PlayAgain()
    {
        GameManager.Instance.GAME_SCORE = 0;
        SceneManager.LoadSceneAsync("Game");
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.GAME_SCORE = 0;
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
