using UnityEngine;
using TMPro;
public class ScoreStoreage : MonoBehaviour
{
    public int Score = 0;
    public TMP_Text ScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreText.text = Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void increaseScore(int value)
    {
        Score += value;
        ScoreText.text = Score.ToString();
    }
}
