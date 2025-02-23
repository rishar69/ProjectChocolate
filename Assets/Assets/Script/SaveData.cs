using System;

[Serializable]
public class LevelData
{
    public int score;
    public float totalNote;
    public float normalHitsTotal;
    public float goodHitsTotal;
    public float perfectHitsTotal;
    public float missHitsTotal;
    public float maxStreak;
    public int multiplier;
}

[Serializable]
public class SaveData
{
    public LevelData level1 = new LevelData();
    public LevelData level2 = new LevelData();
    public LevelData level3 = new LevelData();
}
