using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public static bool rocketsBought = false;
    public static bool autocanonsBought = false;
    public static bool zapperBought = false;
    
    public static bool rocketsEquiped = false;
    public static bool autocanonsEquiped = false;
    public static bool zapperEquiped = false;

    Text rocketPrice;
    Text autocanonsPrice;
    Text zapperPrice;


    private void Start()
    {
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
        if(rocketsBought)
        {
            autocanonsEquiped = false;
            zapperEquiped = false;
            rocketsEquiped = true;
        }

        else
        {
            rocketsBought = true;
        }

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
            autocanonsBought = true;
        }
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
            zapperBought = true;
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
