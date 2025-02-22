using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public GameObject hitStreakUI;
    public GameObject resultsScreen;

    [Header("Gameplay Stats")]
    public int score = 0;
    public float totalNote;
    public float normalHitsTotal;
    public float goodHitsTotal;
    public float perfectHitsTotal;
    public float missHitsTotal;
    public float maxStreak;
    private int currentStreak = 0;
    private int multiplier = 1;

    [Header("Score Values")]
    private const int NORMAL_HIT_POINTS = 50;
    private const int GOOD_HIT_POINTS = 100;
    private const int PERFECT_HIT_POINTS = 200;

    [Header("Results UI")]
    public TextMeshProUGUI hitResultText;
    public TextMeshProUGUI goodResultText;
    public TextMeshProUGUI perfectResultText;
    public TextMeshProUGUI missResultText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI percentageHitText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        totalNote = FindObjectsByType<NoteObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
    }

    public void NoteHit()
    {
        currentStreak++;
        maxStreak = Mathf.Max(maxStreak, currentStreak);
        UpdateUI();
    }

    public void PerfectNote() => UpdateScore(PERFECT_HIT_POINTS, ref perfectHitsTotal);
    public void GoodHit() => UpdateScore(GOOD_HIT_POINTS, ref goodHitsTotal);
    public void NormalHit() => UpdateScore(NORMAL_HIT_POINTS, ref normalHitsTotal);

    public void NoteMiss()
    {
        missHitsTotal++;
        currentStreak = 0;
        UpdateUI();
    }

    private void UpdateScore(int points, ref float hitCounter)
    {
        hitCounter++;
        score += points * multiplier;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        UpdateStreakUI();
        UpdateMultiplier();
    }

    private void UpdateMultiplier()
    {
        multiplier = currentStreak >= 50 ? 6 :
                     currentStreak >= 25 ? 4 :
                     currentStreak >= 10 ? 2 : 1;

        multiplierText.text = $"x{multiplier}";
    }

    private void UpdateStreakUI()
    {
        hitStreakUI.SetActive(currentStreak > 0);
        if (currentStreak > 0)
        {
            hitStreakUI.GetComponent<TextMeshProUGUI>().text = currentStreak.ToString();
        }
    }

    public void LevelFinish()
    {
        resultsScreen.SetActive(true);

        hitResultText.text = normalHitsTotal.ToString();
        goodResultText.text = goodHitsTotal.ToString();
        perfectResultText.text = perfectHitsTotal.ToString();
        missResultText.text = missHitsTotal.ToString();
        finalScoreText.text = score.ToString();

        float totalHits = normalHitsTotal + goodHitsTotal + perfectHitsTotal;
        float percentHit = (totalHits / totalNote) * 100f;
        percentageHitText.text = $"{percentHit:F1}%";

        rankText.text = GetRank(percentHit);
    }

    private string GetRank(float percentHit)
    {
        if (percentHit > 95) return "S";
        if (percentHit > 80) return "A";
        if (percentHit > 70) return "B";
        if (percentHit > 55) return "C";
        if (percentHit > 40) return "D";
        return "F";
    }
}
