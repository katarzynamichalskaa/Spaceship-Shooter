using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;

    public bool rocketsBought;
    public bool autocanonsBought;
    public bool zapperBought;

    public bool rocketsEquiped;
    public bool autocanonsEquiped;
    public bool zapperEquiped;

    public PlayerData( MoneyManager moneyManager)
    {
        coins = MoneyManager.coins;
    }

    public PlayerData( ShopManager shopManager)
    {
        rocketsBought = ShopManager.rocketsBought;
        autocanonsBought = ShopManager.autocanonsBought;
        zapperBought = ShopManager.zapperBought;

        rocketsEquiped = ShopManager.rocketsEquiped;
        autocanonsEquiped = ShopManager.autocanonsEquiped;
        zapperEquiped = ShopManager.zapperEquiped;
    }
}

