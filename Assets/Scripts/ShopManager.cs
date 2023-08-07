using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static bool rocketsBought = false;
    public static bool autocanonsBought = false;
    public static bool zapperBought = false;

    public void BuyRockets()
    {
        rocketsBought = true;
    }
    
    public void BuyAutoCanons()
    {
        autocanonsBought = true;
    }
    
    public void BuyZapper()
    {
        zapperBought = true;
    }
}
