using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public float position_y;
    // Start is called before the first frame update
    void Start()
    {
        GetTheGameObjectName();
        SetColorObstacle();
        speed = Random.Range(10, 16);
        gameObject.transform.position = new Vector3(transform.position.x, position_y, transform.position.z); //set the right y value
    }
    // Update is called once per frame
    void Update()
    {
        ObstacleMovement();
        WheelsRotationAnimation();
    }
    public void ObstacleMovement()
    {
        transform.Translate((Vector3.right * speed) * Time.deltaTime);
    }
    public void SetColorObstacle()
    {
        if (gameObject.GetComponent<Renderer>().material.name.Contains("Car_Paint"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }
    public void WheelsRotationAnimation()
    {
        for (int x = 0; x < gameObject.GetComponentsInChildren<Transform>().Length - 1; x++)
        {
            if (gameObject.transform.GetChild(x).gameObject.name.Contains("Wheel"))
            {
                gameObject.transform.GetChild(x).gameObject.transform.Rotate(0, 0, (-speed * 10) * Time.deltaTime);
            }
        }
    }
    public void GetTheGameObjectName() 
    {       
        if (gameObject.name.Contains("Car_1"))
        {
            position_y = 0.86f;
        }
        if (gameObject.name.Contains("Car_2"))
        {
            position_y = 0.77f;
        }
        if (gameObject.name.Contains("Car_3"))
        {
            position_y = 0.8f;
        }        
        if (gameObject.name.Contains("Car_4"))
        {
            position_y = 1.273f;
        }        
        if (gameObject.name.Contains("Car_5"))
        {
            position_y = 1f;
        }        
        if (gameObject.name.Contains("Car_6"))
        {
            position_y = 0.87f;
        }
        if (gameObject.name.Contains("Bus"))
        {
            position_y = 1.64f;
        }
        if (gameObject.name.Contains("Police"))
        {
            position_y = 0.8f;
        }        
        if (gameObject.name.Contains("Truck_1"))
        {
            position_y = 2f;
        }        
        if (gameObject.name.Contains("Truck_2"))
        {
            position_y = 1.8f;
        }
    }
}
