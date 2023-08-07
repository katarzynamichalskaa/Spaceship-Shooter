using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    private const string RocketsBoughtKey = "RocketsBought";
    private const string AutocanonsBoughtKey = "AutocanonsBought";
    private const string ZapperBoughtKey = "ZapperBought";

    public static bool rocketsBought = false;
    public static bool autocanonsBought = false;
    public static bool zapperBought = false;

    Text rocketPrice;
    Text autocanonsPrice;
    Text zapperPrice;


    private void Start()
    {
        rocketsBought = PlayerPrefs.GetInt(RocketsBoughtKey, 0) == 1;
        autocanonsBought = PlayerPrefs.GetInt(AutocanonsBoughtKey, 0) == 1;
        zapperBought = PlayerPrefs.GetInt(ZapperBoughtKey, 0) == 1;

        rocketPrice = GameObject.Find("RocketPrice").GetComponent<Text>();
        autocanonsPrice = GameObject.Find("AutocanonsPrice").GetComponent<Text>();
        zapperPrice = GameObject.Find("ZapperPrice").GetComponent<Text>();
    }

    void Update()
    {
        UpdateRocketPriceText("100000 $", "2000000 $", "30000000 $");
    }

    public void BuyRockets()
    {
        if (!rocketsBought)
        {
            rocketsBought = true;
            PlayerPrefs.SetInt(RocketsBoughtKey, 1);
        }

        if (rocketsBought)
        {
            //Equip
        }
    }

    public void BuyAutoCanons()
    {
        if (!autocanonsBought)
        {
            autocanonsBought = true;
            PlayerPrefs.SetInt(AutocanonsBoughtKey, 1);
        }

        if (autocanonsBought)
        {
            //Equip
        }
    }

    public void BuyZapper()
    {
        if (!zapperBought)
        {
            zapperBought = true;
            PlayerPrefs.SetInt(ZapperBoughtKey, 1);
        }

        if (zapperBought)
        {
            //Equip
        }
    }

    private void UpdateRocketPriceText(string priceRoc, string priceAuto, string priceZipp)
    {
        string equip = "Equip";

        rocketPrice.text = rocketsBought ? equip : priceRoc;
        autocanonsPrice.text = autocanonsBought ? equip : priceAuto;
        zapperPrice.text = zapperBought ? equip : priceZipp;
    }

}
