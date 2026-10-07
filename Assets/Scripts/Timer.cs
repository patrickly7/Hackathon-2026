using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startingTime = 180f;

    private TMP_Text _timerText;
    private float timeRemaining;

    private bool isTimerFinished;

    public AudioSource startGameWhistleSFX;
    public AudioSource endGameWhistleSFX;

    private void Start()
    {
        _timerText = GetComponent<TMP_Text>();
        timeRemaining = startingTime;
        isTimerFinished = false;

        startGameWhistleSFX.Play();
    }

    private void Update()
    {
        if (isTimerFinished) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isTimerFinished = true;

            endGameWhistleSFX.Play();

            SceneManager.LoadSceneAsync("Results");
        }

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        _timerText.text = $"{minutes:0}:{seconds:00}";
    }
}