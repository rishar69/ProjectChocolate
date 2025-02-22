using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public GameObject hitStreakUI;

    private int scorePerNote = 100;
    private int currentStreak = 0;
    private int normalHit = 50;
    private int goodHit = 100;
    private int PerfectHit = 200;

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
        score += scorePerNote;
        scoreText.text= "Score: " + score;
    }

    public void PerfectNote()
    {
        score += PerfectHit;
        NoteHit();
    }

    public void GoodHit()
    {
        score += goodHit;
        NoteHit();
    }

    public void NormalHit()
    {
        score += normalHit;
        NoteHit();
    }

    public void NoteMiss()
    {
        currentStreak = 0 ;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(currentStreak == 0)
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
