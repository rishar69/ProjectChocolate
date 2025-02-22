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
    }

    public void PerfectNote()
    {
        score += perfectHit;
    }

    public void GoodHit()
    {
        score += goodHit;
    }

    public void NormalHit()
    {
        score += normalHit;
    }

    public void NoteMiss()
    {
        currentStreak = 0;
        UpdateStreakUI();
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
