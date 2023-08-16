using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    [SerializeField] Text money;
    static int coins = 0;
    int earnedCoins = 0;
    ScoreManager scoreManager;

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

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOverDashboard")
        {
            money = GameObject.Find("YourMoney").GetComponent<Text>();
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            money.text = coins.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Shop")
        {
            money = GameObject.Find("YourMoney").GetComponent<Text>();
            money.text = coins.ToString();
        }
    }

    public void UpdateCoinText()
    {
        if (scoreManager != null)
        {
            earnedCoins = scoreManager.ReturnEarnedCoins();

            if (coins < coins + earnedCoins)
            {
                coins += Mathf.RoundToInt(5f);
                money.text = coins.ToString();

            }
            else
            {
                coins = earnedCoins + coins;
                money.text = coins.ToString();

            }
        }
    }
}
