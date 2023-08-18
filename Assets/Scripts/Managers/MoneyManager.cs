using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    [SerializeField] Text money;
    public static int coins = 0;
    int purchaseCost;
    int currentCoins;
    ScoreManager scoreManager;

    void Start()
    {
        LoadMoney();
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
        if (scoreManager != null && SceneManager.GetActiveScene().name == "GameOverDashboard")
        {
            coins += Mathf.RoundToInt(5f);
            money.text = coins.ToString();
            SaveMoney();
        }

        else if(SceneManager.GetActiveScene().name == "Shop")
        {
            money.text = coins.ToString();
        }
    }

    public bool Purchase(int cost)
    {
        currentCoins = coins;
        purchaseCost = cost;

        if(purchaseCost < coins)
        {
            InvokeRepeating("DecreaseCoinsOverTime", 0.1f, 0.1f);
            return true;
        }
        else
        {
            UnityEngine.Debug.Log("nie masz kaski");
            return false;
        }
        return false;
    }

    public void SaveMoney()
    {
        SaveSystem.SavePlayerMoney(this);
    }

    public void LoadMoney()
    {
        PlayerData data = SaveSystem.LoadPlayerMoney(this);

        coins = data.coins;
    }

    private void DecreaseCoinsOverTime()
    {
        if (coins <= currentCoins - purchaseCost)
        {
            CancelInvoke("DecreaseCoinsOverTime");
            coins = currentCoins - purchaseCost;
            UpdateCoinText();
            SaveMoney();
            
        }
        else
        {
            coins -= Mathf.RoundToInt(1f);
            UpdateCoinText();
        }
    }
}
