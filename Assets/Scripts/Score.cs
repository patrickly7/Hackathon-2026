using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _scoreText.text = GameManager.Instance.GAME_SCORE.ToString();
    }
}