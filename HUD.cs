using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text CoinsPoint;
    private int coins;

    public void IncreaseCoins()
    {
        coins += 1;
        CoinsPoint.text = coins.ToString();
    }
    
}
