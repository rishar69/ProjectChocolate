using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public GameObject hitStreakUI;
    public GameObject ResultsScreen;

    public int score = 0;
    public float totalNote;
    public float normalHitsTotal;
    public float goodHitsTotal;
    public float perfectHitsTotal;
    public float missHitsTotal;
    public float maxStreak;

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

    private void Start()
    {
        totalNote = FindObjectsByType<NoteObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
    }


    public void NoteHit()
    {
        currentStreak++;

        if (currentStreak > maxStreak)
        {
            maxStreak = currentStreak;
        }

        scoreText.text = "Score: " + score;
        UpdateStreakUI();
        UpdateMultiplier();
    }

    public void PerfectNote()
    {
        score += perfectHit * multiplier;
        perfectHitsTotal ++;
    }

    public void GoodHit()
    {
        score += goodHit * multiplier;
        goodHitsTotal++;
    }

    public void NormalHit()
    {
        normalHitsTotal++;
        score += normalHit * multiplier;
    }

    public void NoteMiss()
    {
        missHitsTotal++;
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
