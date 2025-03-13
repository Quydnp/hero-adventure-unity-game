using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    private TMP_Text scoreText;
    private int currentScore;

    const string SCORE_AMOUNT_TEXT = "Score Amount Text";

    public int CurrentScore { get { return currentScore; } }
    public void SetCurrentScore(int value)
    {
        currentScore = value;
        if (scoreText == null)
        {
            scoreText = GameObject.Find(SCORE_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        scoreText.text = currentScore.ToString();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;

        if (scoreText == null)
        {
            scoreText = GameObject.Find(SCORE_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        scoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        currentScore = 0;

        if (scoreText == null)
        {
            scoreText = GameObject.Find(SCORE_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        scoreText.text =currentScore.ToString();
    }
}
