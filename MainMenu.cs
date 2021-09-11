using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour //and store
{
    public GameObject[] cars;
    public GameObject canvas;
    public string currentScene;
    public int currentCar;
    private int coinsNumber;
    public Text coinsText;
    public Text carName;
    public Text carPriceText;
    public int[] carPrices;
    public Text select;
    void Start()
    {
        SetCurrentScene("MainMenu");
        cars[PlayerPrefs.GetInt("SelectedCar", 0)].SetActive(true);
        coinsNumber = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coinsNumber.ToString();
        currentCar = PlayerPrefs.GetInt("SelectedCar", 0);
    }
    public void Update()
    {
        InStore();
    }
    private void SetCurrentScene(string scene)
    {
        currentScene = scene;
        for (int i = 1; i < canvas.gameObject.GetComponentsInChildren<Transform>().Length - 1; i++)//clean the HUD
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (scene == "MainMenu") //activated the main menu
        {
            canvas.transform.GetChild(1).gameObject.SetActive(true); //GetChild(1) = MainMenu gameobject
        }
        if (scene == "Store")//activated the store
        {
            canvas.transform.GetChild(2).gameObject.SetActive(true); //GetChild(2) = Store gameobject
        }
    }
    public void MainMenuButtons()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "PlayButton")
        {
            SceneManager.LoadScene(1); //start gameplay
        }        
        if (EventSystem.current.currentSelectedGameObject.name == "Settings")
        {
            //TODO: LOAD THE SETTINGS
        }        
        if (EventSystem.current.currentSelectedGameObject.name == "Store")
        {
            SetCurrentScene("Store"); //load the store
        }        
        if (EventSystem.current.currentSelectedGameObject.name == "Close")
        {
            Application.Quit(); //close the game
        }
    }
    public void StoreButtons()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "RightArrow")
        {
            ChangeTheCarInStore(1);
        }
        if (EventSystem.current.currentSelectedGameObject.name == "LeftArrow")
        {
            ChangeTheCarInStore(-1);
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Select")
        {
            if (select.text == "Select")
            {
                SaveTheSelectCar();
                SetCurrentScene("MainMenu");
            }
            else if (select.text == "Buy")
            {
                if (cars[currentCar].activeSelf)
                {
                    if (coinsNumber >= carPrices[currentCar])
                    {
                        PlayerPrefs.SetString(cars[currentCar].name, "Bought");
                        coinsNumber -= carPrices[currentCar];
                        PlayerPrefs.SetInt("Coins", coinsNumber);
                        coinsText.text = coinsNumber.ToString();
                    }
                }
            }
        }
    }
    private void ChangeTheCarInStore(int nextCarDirection)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (cars[i].activeSelf)
            {
                if (cars[cars.Length - 1].activeSelf && nextCarDirection == 1) //if in the limited of list / 1 = right button
                {
                    cars[i].SetActive(false);
                    cars[0].SetActive(true);
                    currentCar = 0;
                }
                else if (cars[0].activeSelf && nextCarDirection == -1) //if in the limited of list / -1 = left button
                {
                    cars[i].SetActive(false);
                    cars[cars.Length - 1].SetActive(true);
                    currentCar = cars.Length - 1;
                }
                else 
                {
                    cars[i].SetActive(false);
                    cars[i + (nextCarDirection)].SetActive(true);
                    currentCar += nextCarDirection;
                }
                break; 
            }
        }
    }
    private void SaveTheSelectCar()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (cars[i].activeSelf)
            {
                PlayerPrefs.SetInt("SelectedCar", i);
                PlayerPrefs.Save();
            }
        }
    }
    public void InStore()
    {
        if (currentScene == "Store")
        {
            if (cars[currentCar].activeSelf)
            {
                if (carName.text == cars[currentCar].name)//optimization
                {
                    return;
                }
                carName.text = cars[currentCar].name;
                carPriceText.text = carPrices[currentCar].ToString();
                CanBuy();
                Debug.Log("a");
            }
        }
    } 

    public void CanBuy()
    {
        if (CheckCanBuy(cars[currentCar].name) == true)
        {
            select.text = "Buy";
        }
        else
        {
            select.text = "Select";
        }
    }
    private bool CheckCanBuy(string currentCar)
    {
        string canBuy = PlayerPrefs.GetString(currentCar);

        if (canBuy == "Bought")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
