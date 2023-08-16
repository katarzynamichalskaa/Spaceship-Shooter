using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public static int coinsEarned;

    [SerializeField] Text scoreText;         
    [SerializeField] float scoreIncreaseRate = 1.0f;

    MoneyManager moneyManager;
    private int score = 0; 
    private float timeElapsed = 0.0f;
    bool converted = false;

    private void Start()
    {
        UpdateScoreText();

        InvokeRepeating("IncreaseScoreOverTime", 1.0f, 1.0f);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOverDashboard" && !converted)
        {
            //coin
            moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

            //score
            CancelInvoke("IncreaseScoreOverTime");
            scoreText = GameObject.Find("Score").GetComponent<Text>();
            UpdateScoreText();
            InvokeRepeating("DecreaseScoreOverTime", 0.1f, 0.1f);

            converted = true;
        }

        else if(SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(gameObject);
        }
    }

    private void IncreaseScoreOverTime()
    {
        timeElapsed += 1.0f;
        score += Mathf.RoundToInt(scoreIncreaseRate * timeElapsed);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if(score <= 0)
        {
            score = 0;
        }

        scoreText.text =  score.ToString();
    }

    private void DecreaseScoreOverTime()
    {
        if (score > 0)
        {
            score -= Mathf.RoundToInt(5f);
            UpdateScoreText();
            moneyManager.UpdateCoinText();
        }
        else
        {
            score = 0;
            UpdateScoreText();
            CancelInvoke("DecreaseScoreOverTime");
        }
    }

    public int ReturnEarnedCoins()
    {
        coinsEarned = score / 1;

        return coinsEarned;
    }

}
