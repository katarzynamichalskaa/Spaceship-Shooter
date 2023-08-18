using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public static bool rocketsBought = false;
    public static bool autocanonsBought = false;
    public static bool zapperBought = false;
    
    public static bool rocketsEquiped = false;
    public static bool autocanonsEquiped = false;
    public static bool zapperEquiped = false;

    Text rocketPrice;
    Text autocanonsPrice;
    Text zapperPrice;

    MoneyManager moneyManager;


    private void Start()
    {
        LoadBoughtAndEquipped();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            rocketPrice = GameObject.Find("RocketPrice").GetComponent<Text>();
            autocanonsPrice = GameObject.Find("AutocanonsPrice").GetComponent<Text>();
            zapperPrice = GameObject.Find("ZapperPrice").GetComponent<Text>();

            UpdateRocketPriceText("100000 $", "2000000 $", "30000000 $");
        }
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

    public void BuyRockets()
    {
        if(rocketsBought)
        {
            autocanonsEquiped = false;
            zapperEquiped = false;
            rocketsEquiped = true;
        }

        else
        {
            moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

            bool purchased = moneyManager.Purchase(100000);
            if(purchased)
            {
                rocketsBought = true;
                rocketsEquiped = true;
            }
        }

        SaveBoughtAndEquipped();

    }

    public void BuyAutoCanons()
    {
        if (autocanonsBought)
        {
            autocanonsEquiped = true;
            zapperEquiped = false;
            rocketsEquiped = false;
        }

        else
        {
            moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

            bool purchased = moneyManager.Purchase(2000000);
            if(purchased)
            {
                autocanonsBought = true;
                autocanonsEquiped = true;
            }
        }

        SaveBoughtAndEquipped();

    }

    public void BuyZapper()
    {
        if (zapperBought)
        {
            autocanonsEquiped = false;
            zapperEquiped = true;
            rocketsEquiped = false;
        }

        else
        {
            moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

            bool purchased = moneyManager.Purchase(30000000);
            if(purchased)
            {
                zapperBought = true;
                zapperEquiped = true;
            }
        }

        SaveBoughtAndEquipped();

    }

    private void UpdateRocketPriceText(string priceRoc, string priceAuto, string priceZipp)
    {
        string equip = "Equip";
        rocketPrice.text = rocketsBought ? equip : priceRoc;
        autocanonsPrice.text = autocanonsBought ? equip : priceAuto;
        zapperPrice.text = zapperBought ? equip : priceZipp;
    }

    public void SaveBoughtAndEquipped()
    {
        SaveSystem.SavePlayerEquipment(this);
    }

    public void LoadBoughtAndEquipped()
    {
        PlayerData data = SaveSystem.LoadPlayerEquipment(this);

        rocketsBought = data.rocketsBought;
        autocanonsBought = data.autocanonsBought;
        zapperBought = data.zapperBought;

        rocketsEquiped = data.rocketsEquiped;
        autocanonsEquiped = data.autocanonsEquiped;
        zapperEquiped = data.zapperEquiped;
    }
}
