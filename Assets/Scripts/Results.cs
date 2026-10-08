using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    public AudioSource StatSFX;
    public AudioSource SRankSFX;
    public AudioSource ARankSFX;
    public AudioSource BRankSFX;
    public AudioSource CRankSFX;
    public AudioSource DRankSFX;
    public AudioSource FRankSFX;

    [SerializeField] private GameObject nearMintCount;
    [SerializeField] private GameObject lightlyPlayedCount;
    [SerializeField] private GameObject moderatelyPlayedCount;
    [SerializeField] private GameObject heavilyPlayedCount;
    [SerializeField] private GameObject damagedCount;

    [SerializeField] private GameObject rank;
    [SerializeField] private GameObject playAgainButton;
    [SerializeField] private GameObject mainMenuButton;

    private int _nearMintCount;
    private int _lightlyPlayedCount;
    private int _moderatelyPlayedCount;
    private int _heavilyPlayedCount;
    private int _damagedCount;

    private IEnumerator Start()
    {
        // NEAR MINT
        yield return new WaitForSeconds(1f);
        StatSFX.Play();

        _nearMintCount = GameManager.Instance.NEAR_MINT_COUNT;

        nearMintCount.GetComponent<TMP_Text>().text = "Near Mint: " + _nearMintCount.ToString();
        nearMintCount.SetActive(true);

        // LIGHTLY PLAYED
        yield return new WaitForSeconds(1f);
        StatSFX.Play();

        _lightlyPlayedCount = GameManager.Instance.LIGHTLY_PLAYED_COUNT;

        lightlyPlayedCount.GetComponent<TMP_Text>().text = "Lightly Played: " + _lightlyPlayedCount.ToString();
        lightlyPlayedCount.SetActive(true);

        // MODERATELY PLAYED
        yield return new WaitForSeconds(1f);
        StatSFX.Play();

        _moderatelyPlayedCount = GameManager.Instance.MODERATELY_PLAYED_COUNT;

        moderatelyPlayedCount.GetComponent<TMP_Text>().text = "Moderately Played: " + _moderatelyPlayedCount.ToString();
        moderatelyPlayedCount.SetActive(true);

        // HEAVILY PLAYED
        yield return new WaitForSeconds(1f);
        StatSFX.Play();

        _heavilyPlayedCount = GameManager.Instance.HEAVILY_PLAYED_COUNT;

        heavilyPlayedCount.GetComponent<TMP_Text>().text = "Heavily Played: " + _heavilyPlayedCount.ToString();
        heavilyPlayedCount.SetActive(true);

        // DAMAGED
        yield return new WaitForSeconds(1f);
        StatSFX.Play();

        _damagedCount = GameManager.Instance.DAMAGED_COUNT;

        damagedCount.GetComponent<TMP_Text>().text = "Damaged: " + _damagedCount.ToString();
        damagedCount.SetActive(true);

        // Display your final rank
        yield return new WaitForSeconds(1f);

        rank.GetComponent<TMP_Text>().text = CalculateRank();
        rank.SetActive(true);

        playAgainButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    private string CalculateRank()
    {
        var score = GameManager.Instance.GAME_SCORE;

        if (score >= 120 && _damagedCount == 0 && _heavilyPlayedCount == 0 && _moderatelyPlayedCount == 0)
        {
            SRankSFX.Play();
            return "S";
        }
        else if (score >= 100 && _damagedCount <= 1)
        {
            ARankSFX.Play();
            return "A";
        }
        else if (score >= 80 && _damagedCount <= 2)
        {
            BRankSFX.Play();
            return "B";
        }
        else if (score >= 60 && _damagedCount <= 3)
        {
            CRankSFX.Play();
            return "C";
        }
        else if (score >= 40 && _damagedCount <= 4)
        {
            DRankSFX.Play();
            return "D";
        }
        else
        {
            FRankSFX.Play();
            return "F";
        }
    }

    public void PlayAgain()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadSceneAsync("Game");
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
