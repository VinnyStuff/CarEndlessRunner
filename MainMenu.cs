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
    public string currentCar;
    public Text coins;
    public Text carName;
    void Start()
    {
        CreatingStructArray();
        SetCurrentScene("MainMenu");
        cars[PlayerPrefs.GetInt("SelectedCar", 0)].SetActive(true);
        coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        currentCar = cars[PlayerPrefs.GetInt("SelectedCar", 0)].name;
    }
    public void Update()
    {
        InStore();
    }
    private void SetCurrentScene(string scene)
    {
        currentScene = scene;
        for (int i = 1; i < canvas.gameObject.GetComponentsInChildren<Transform>().Length; i++)//clean the HUD
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
            SaveTheSelectCar();
            SetCurrentScene("MainMenu");
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
                    currentCar = cars[0].name.ToString();
                }
                else if (cars[0].activeSelf && nextCarDirection == -1) //if in the limited of list / -1 = left button
                {
                    cars[i].SetActive(false);
                    cars[cars.Length - 1].SetActive(true);
                    currentCar = cars[cars.Length - 1].name.ToString();
                }
                else 
                {
                    cars[i].SetActive(false);
                    cars[i + (nextCarDirection)].SetActive(true);
                    currentCar = cars[i + (nextCarDirection)].name.ToString();
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
            for(int i = 0; i < CarsArray.Length; i++)
            {
                if (currentCar == CarsArray[i].carTitle)
                {
                    if (carName.text != CarsArray[i].carTitle)//optimized (run one time)
                    {
                        carName.text = CarsArray[i].carTitle;
                    }
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
    //--- Learning Structs
    public struct Cars
    {
        public string carTitle;
        public int price;
        public int speed;
    }

    Cars car0;
    Cars car1;
    Cars car2;
    Cars car3;
    Cars car4;

    public Cars[] CarsArray = new Cars[5];
    public void CreatingStructArray()
    {
        car0.carTitle = "Car2";

        car1.carTitle = "SportCar2";

        car2.carTitle = "Jeep2";

        car3.carTitle = "MicroBus4";

        car4.carTitle = "Sedan1";


        CarsArray[0] = car0;
        CarsArray[1] = car1;
        CarsArray[2] = car2;
        CarsArray[3] = car3;
        CarsArray[4] = car4;
    }
    //----------------------------
}
