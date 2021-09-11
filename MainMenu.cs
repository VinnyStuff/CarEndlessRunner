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
    private int coinsNumber;
    public Text coinsText;
    public Text carName;
    public Text carPrice;
    public Text select;
    void Start()
    {
        SetCurrentScene("MainMenu");
        cars[PlayerPrefs.GetInt("SelectedCar", 0)].SetActive(true);
        coinsNumber = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coinsNumber.ToString();
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
                if (cars[0].activeSelf)
                {
                    if (coinsNumber >= 5)
                    {
                        PlayerPrefs.SetString(cars[0].name, "Bought");
                        coinsNumber -= 5;
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
                }
                else if (cars[0].activeSelf && nextCarDirection == -1) //if in the limited of list / -1 = left button
                {
                    cars[i].SetActive(false);
                    cars[cars.Length - 1].SetActive(true);
                }
                else 
                {
                    cars[i].SetActive(false);
                    cars[i + (nextCarDirection)].SetActive(true);
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
            if (cars[0].activeSelf)
            {
                carName.text = "";
                if (CanBuy(cars[0].name) == true)
                {
                    select.text = "Buy";
                }
                else
                {
                    select.text = "Select";
                }
            }
            else if (cars[1].activeSelf)
            {
                carName.text = "";
                if (CanBuy(cars[1].name) == true)
                {
                    select.text = "Buy";
                }
                else
                {
                    select.text = "Select";
                }
            }
            else if (cars[2].activeSelf)
            {
                carName.text = "";
                if (CanBuy(cars[2].name) == true)
                {
                    select.text = "Buy";
                }
                else
                {
                    select.text = "Select";
                }
            }
            else if (cars[3].activeSelf)
            {
                carName.text = "";
                if (CanBuy(cars[3].name) == true)
                {
                    select.text = "Buy";
                }
                else
                {
                    select.text = "Select";
                }
            }
            else if (cars[4].activeSelf)
            {
                carName.text = "";
                if (CanBuy(cars[4].name) == true)
                {
                    select.text = "Buy";
                }
                else
                {
                    select.text = "Select";
                }
            }
        }
    } 

    private bool CanBuy(string currentCar)
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
