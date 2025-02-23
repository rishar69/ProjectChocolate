using System.IO;
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
    public int currentLevel = 1;

    [Header("Save File level")]
    public LevelData level1 = new LevelData();
    public LevelData level2 = new LevelData();
    public LevelData level3 = new LevelData();

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
    private const int NORMAL_HIT_POINTS = 100;
    private const int GOOD_HIT_POINTS = 200;
    private const int PERFECT_HIT_POINTS = 300;
    private string savePath;


    [Header("Results UI")]
    public TextMeshProUGUI hitResultText;
    public TextMeshProUGUI goodResultText;
    public TextMeshProUGUI perfectResultText;
    public TextMeshProUGUI missResultText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI percentageHitText;

    private LevelData GetCurrentLevelData()
    {
        return currentLevel == 1 ? level1 :
               currentLevel == 2 ? level2 :
               level3;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        savePath = Application.persistentDataPath + "/savefile.dat";
    }

    private void Start()
    {
        totalNote = FindObjectsByType<NoteObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
        ResetGameStats();
    }

    public void SaveGame()
    {
        LevelData currentData = GetCurrentLevelData();

        currentData.score = score;
        currentData.totalNote = totalNote;
        currentData.normalHitsTotal = normalHitsTotal;
        currentData.goodHitsTotal = goodHitsTotal;
        currentData.perfectHitsTotal = perfectHitsTotal;
        currentData.missHitsTotal = missHitsTotal;
        currentData.maxStreak = maxStreak;
        currentData.multiplier = multiplier;

        using (BinaryWriter writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
        {
            writer.Write(currentLevel);
            SaveLevel(writer, level1);
            SaveLevel(writer, level2);
            SaveLevel(writer, level3);
        }

        Debug.Log($"Game Saved! Level: {currentLevel}");
        LogLevelData();
    }




    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
            {
                currentLevel = reader.ReadInt32();
                LoadLevel(reader, level1);
                LoadLevel(reader, level2);
                LoadLevel(reader, level3);
            }

            // Apply loaded data
            LevelData currentData = GetCurrentLevelData();

            // Ensure previous data is reset before loading new values
            ResetGameStats();

            // Assign loaded values
            score = currentData.score;
            totalNote = currentData.totalNote;
            normalHitsTotal = currentData.normalHitsTotal;
            goodHitsTotal = currentData.goodHitsTotal;
            perfectHitsTotal = currentData.perfectHitsTotal;
            missHitsTotal = currentData.missHitsTotal;
            maxStreak = currentData.maxStreak;
            multiplier = currentData.multiplier;

            Debug.Log($"Game Loaded! Current Level: {currentLevel}");
            LogLevelData();
        }
        else
        {
            Debug.LogWarning("No save file found.");
        }
    }

    public void ShowLevelStats()
    {
        LoadGame();
        LevelData currentData = GetCurrentLevelData();

        // Example: Update UI elements with previous stats
        scoreText.text = $"Last Score: {currentData.score}";
    }
    void LogLevelData(int levelNumber, LevelData level)
    {
        Debug.Log($" Level {levelNumber} Data:");
        Debug.Log($"   - Score: {level.score}");
        Debug.Log($"   - Normal Hits: {level.normalHitsTotal}");
        Debug.Log($"   - Good Hits: {level.goodHitsTotal}");
        Debug.Log($"   - Perfect Hits: {level.perfectHitsTotal}");
        Debug.Log($"   - Missed Hits: {level.missHitsTotal}");
        Debug.Log($"   - Max Streak: {level.maxStreak}");
    }

    private void SaveLevel(BinaryWriter writer, LevelData level)
    {
        writer.Write(level.score);
        writer.Write(level.totalNote);
        writer.Write(level.normalHitsTotal);
        writer.Write(level.goodHitsTotal);
        writer.Write(level.perfectHitsTotal);
        writer.Write(level.missHitsTotal);
        writer.Write(level.maxStreak);
        writer.Write(level.multiplier);
    }

    private void LoadLevel(BinaryReader reader, LevelData level)
    {
        level.score = reader.ReadInt32();
        level.totalNote = reader.ReadSingle();
        level.normalHitsTotal = reader.ReadSingle();
        level.goodHitsTotal = reader.ReadSingle();
        level.perfectHitsTotal = reader.ReadSingle();
        level.missHitsTotal = reader.ReadSingle();
        level.maxStreak = reader.ReadSingle();
        level.multiplier = reader.ReadInt32();
    }

    private void LogLevelData()
    {
        Debug.Log("--- Saved Data ---");
        LogSingleLevelData(1, level1);
        LogSingleLevelData(2, level2);
        LogSingleLevelData(3, level3);
    }

    private void LogSingleLevelData(int levelNumber, LevelData level)
    {
        Debug.Log($"Level {levelNumber}: Score={level.score}, Max Streak={level.maxStreak}, Normal Hits={level.normalHitsTotal}, Good Hits={level.goodHitsTotal}, Perfect Hits={level.perfectHitsTotal}, Misses={level.missHitsTotal}");
    }

    private void ResetGameStats()
    {
        score = 0;
        normalHitsTotal = 0;
        goodHitsTotal = 0;
        perfectHitsTotal = 0;
        missHitsTotal = 0;
        maxStreak = 0;
        currentStreak = 0;
        multiplier = 1;
        UpdateUI();
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

        SaveGame();
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

