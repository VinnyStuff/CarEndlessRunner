using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public float positionY;
    // Start is called before the first frame update
    void Start()
    {
        GetTheGameObjectName();
        gameObject.transform.position = new Vector3(0, positionY, 0);

        gameObject.AddComponent<BoxCollider>();
        speed = Random.Range(8, 11);
        //transform.rotation = Quaternion.Euler(0, 180, 55);
        //gameObject.transform.Rotate(0, 0, rotateForce * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        WheelsRotationAnimation();
    }
    public void WheelsRotationAnimation()
    {
        for (int x = 0; x < gameObject.GetComponentsInChildren<Transform>().Length - 1; x++)
        {
            gameObject.transform.GetChild(x).gameObject.transform.Rotate(0, 0, (-speed * 10) * Time.deltaTime);
        }
    }
    public void GetTheGameObjectName()
    {       
        if (gameObject.name.Contains("Car_1"))
        {
            positionY = 0.86f;
        }
        if (gameObject.name.Contains("Car_2"))
        {
            positionY = 0.77f;
        }
        if (gameObject.name.Contains("Car_3"))
        {
            positionY = 0.8f;
        }        
        if (gameObject.name.Contains("Car_4"))
        {
            positionY = 1.273f;
        }        
        if (gameObject.name.Contains("Car_5"))
        {
            positionY = 1f;
        }        
        if (gameObject.name.Contains("Car_6"))
        {
            positionY = 0.87f;
        }
        if (gameObject.name.Contains("Bus"))
        {
            positionY = 1.64f;
        }
        if (gameObject.name.Contains("Police"))
        {
            positionY = 0.8f;
        }        
        if (gameObject.name.Contains("Truck_1"))
        {
            positionY = 2f;
        }        
        if (gameObject.name.Contains("Truck_2"))
        {
            positionY = 1.8f;
        }
    }
}
