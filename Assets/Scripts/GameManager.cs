using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int GAME_SCORE = 0;
    public int NEAR_MINT_COUNT = 0;
    public int LIGHTLY_PLAYED_COUNT = 0;
    public int MODERATELY_PLAYED_COUNT = 0;
    public int HEAVILY_PLAYED_COUNT = 0;
    public int DAMAGED_COUNT = 0;

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

    public void AddNearMint() => NEAR_MINT_COUNT++;
    public void AddLightlyPlayed() => LIGHTLY_PLAYED_COUNT++;
    public void AddModeratelyPlayed() => MODERATELY_PLAYED_COUNT++;
    public void AddHeavilyPlayed() => HEAVILY_PLAYED_COUNT++;
    public void AddDamaged() => DAMAGED_COUNT++;

    public void ResetGame()
    {
        // Reset score
        GAME_SCORE = 0;

        // Reset counts
        NEAR_MINT_COUNT = 0;
        LIGHTLY_PLAYED_COUNT= 0;
        MODERATELY_PLAYED_COUNT = 0;
        HEAVILY_PLAYED_COUNT = 0;
        DAMAGED_COUNT = 0;
    }
}