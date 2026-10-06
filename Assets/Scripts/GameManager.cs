using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int GAME_SCORE = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        GAME_SCORE += amount;
    }
}