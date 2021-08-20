using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public bool CanAccelerate;
    // Start is called before the first frame update
    void Start()
    {
        CanAccelerate = false;
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();
        for (int x = 0; x < gameObject.GetComponentsInChildren<Transform>().Length - 1; x++)
        {
            gameObject.transform.GetChild(x).gameObject.AddComponent<BoxCollider>();
        }
        speed = Random.Range(8, 11);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAccelerate == true)
        {
            //rb.velocity = Vector3.forward * speed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        CanAccelerate = true;
    }
}
