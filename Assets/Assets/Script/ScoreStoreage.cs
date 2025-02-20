using UnityEngine;
using TMPro;
public class ScoreStoreage : MonoBehaviour
{
    public bool reset;
    public int Score = 0;
    public int HighScore = 0;
    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (reset)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        ScoreText.text = Score.ToString() + " : Score";
        HighScoreText.text = HighScore.ToString() + " : High Score";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void increaseScore(int value)
    {
        Score += value;
        ScoreText.text = Score.ToString() + " : Score";
        if (HighScore < Score)
        {
            HighScore = Score;
            HighScoreText.text = HighScore.ToString() + " : High Score";
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }
}
