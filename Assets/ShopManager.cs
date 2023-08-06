using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static bool rocketsBought = false;

    // Update is called once per frame
    void Start()
    {
        
    }

    public void BuyRockets()
    {
        rocketsBought = true;
    }
}
