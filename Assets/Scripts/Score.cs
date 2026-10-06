using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreText.text = GameManager.Instance.GAME_SCORE.ToString();
    }
}