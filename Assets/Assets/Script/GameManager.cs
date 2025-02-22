using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public GameObject hitStreakUI;

    //private int scorePerNote = 100;
    private int currentStreak = 0;
    private int normalHit = 50;
    private int goodHit = 100;
    private int perfectHit = 200;
    private int multiplier = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NoteHit()
    {
        currentStreak++;
        scoreText.text = "Score: " + score;
        UpdateStreakUI();
        UpdateMultiplier();
    }

    public void PerfectNote()
    {
        score += perfectHit * multiplier;
    }

    public void GoodHit()
    {
        score += goodHit * multiplier;
    }

    public void NormalHit()
    {
        score += normalHit * multiplier;
    }

    public void NoteMiss()
    {
        currentStreak = 0;
        UpdateStreakUI();
        UpdateMultiplier();
    }


    private void UpdateMultiplier()
    {
        if (currentStreak >= 50)
        {
            multiplier = 6;
        }
        else if (currentStreak >= 25)
        {
            multiplier = 4;
        }
        else if (currentStreak >= 10)
        {
            multiplier = 2;
        }
        else
        {
            multiplier = 1;
        }

        multiplierText.text = "x" + multiplier.ToString();
    }

    private void UpdateStreakUI()
    {
        if (currentStreak == 0)
        {
            hitStreakUI.SetActive(false);
        }
        else
        {
            hitStreakUI.SetActive(true);
            hitStreakUI.GetComponent<TextMeshProUGUI>().text = currentStreak.ToString();
        }
    }
}
