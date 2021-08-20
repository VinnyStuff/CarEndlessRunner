using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        AddRigidbody();
        speed = Random.Range(8, 11);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.forward * speed;
    }
    void AddRigidbody() //and get
    {
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
            rb.GetComponent<Rigidbody>();
        }
    }
}
