using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject[] cars;
    public GameObject canvas;
    void Start()
    {
        SetCurrentScene("MainMenu");
        cars[PlayerPrefs.GetInt("SelectedCar", 0)].SetActive(true);
    }
    private void SetCurrentScene(string scene)
    {
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
}
