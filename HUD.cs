using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text CoinsPoint;
    private int coins;

    public void CleanHUD()
    {
        for (int i = 1; i < gameObject.gameObject.GetComponentsInChildren<Transform>().Length; i++)//clean the HUD
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void SetCurrentScene(string scene)
    {
        CleanHUD();
        if (scene == "Gameplay")
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (scene == "Pause")//activated the store
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    public void IncreaseCoins()
    {
        coins += 1;
        CoinsPoint.text = coins.ToString();
    }
    public void PauseButton()
    {
        if (Time.timeScale == 1)//pause
        {
            Time.timeScale = 0;
            SetCurrentScene("Pause");
        }        
        else if (Time.timeScale == 0)//unpause
        {
            Time.timeScale = 1;
            SetCurrentScene("Gameplay");
        }
    }
}
