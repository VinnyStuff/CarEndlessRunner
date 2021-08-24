using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;// = 15f;
    public float currentSpeed;
    public float laneSpeed;// = 10f;
    private Rigidbody rb;
    private int currentLane = 4;
    private Vector3 verticalTargetPosition;
    public bool playerIsDead;
    public List<GameObject> cars;
    public float velocityIncreasingPerSecond;
    //NITRO
    public float speedBefoneNitro;
    public float nitroDurationInSeconds;
    public float nitroDuration;
    public bool canUseNitro;
    public Camera playerCamera;
    public Vector3 playerCameraStartPosition;
    //
    public bool canRotateRight;
    public bool canRotateLeft;
    void Start()
    {
        back = false;
        playerCameraStartPosition = playerCamera.transform.localPosition;
        nitroDuration = 0;
        SelectCar();
        rb = GetComponent<Rigidbody>();
        playerIsDead = false;
        currentSpeed = speed;
    }
    public bool back;
    private void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.W))
        {
            canUseNitro = true;
            speedBefoneNitro = currentSpeed;
        }
        if (Input.GetKeyDown(KeyCode.D))//right
        {
            //canRotateRight = true;
        }        
        if (Input.GetKeyDown(KeyCode.A))//right
        {
            //canRotateLeft = true;
        }
        if (canRotateRight)
        {
            if (cars[0].transform.localRotation.eulerAngles.y <= 40 && back == false)
            {
                cars[0].transform.Rotate(0, 40 * Time.deltaTime, 0);
            }
            else
            {
                back = true;
            }
            if (back)
            {
                cars[0].transform.Rotate(0, -40 * Time.deltaTime, 0);
            }
            if (back == true && cars[0].transform.localRotation.eulerAngles.y >= 300 && cars[0].transform.localRotation.eulerAngles.y <= 320)
            {
                cars[0].transform.Rotate(0, 40 * Time.deltaTime, 0);
            }
            if (back == true && cars[0].transform.localRotation.eulerAngles.y >= 355)
            {
                cars[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
                canRotateRight = false;
                back = false;
            }
        }
        CameraFollowPlayer();
    }
    public void FixedUpdate()
    {
        IncreasePlayerSpeedOvertime();
        TurnOnNitro();
    }
    public float virtualCameraX;
    public void CameraFollowPlayer()
    {
        float playerPositionX = gameObject.transform.position.x;
        virtualCameraX = (virtualCameraX * 0.9f) + (playerPositionX * 0.1f);
        float cameraParallax = 0.5f;
        float heightCamera = 5.36f;
        float playerPositionZ = gameObject.transform.position.z - 9.8f;
        playerCamera.transform.position = new Vector3(virtualCameraX * cameraParallax, heightCamera, playerPositionZ);
    }
    public void SelectCar()
    {
        for (int i = cars.Count - 1; i >= 0; i--)
        {
            int selectedCar = PlayerPrefs.GetInt("SelectedCar", 0);
            if (i == selectedCar)
            {
                cars[selectedCar].SetActive(true);
            }
            else
            {
                Destroy(cars[i]);
                cars.RemoveAt(i);
            }
        }
    }
    public void IncreasePlayerSpeedOvertime()
    {
        if (canUseNitro == false && playerIsDead == false)
        {
            currentSpeed = currentSpeed + (velocityIncreasingPerSecond / 50);
        }
    }
    public void TurnOnNitro() //Nitro final speed = current speed + 10
    {
        if (canUseNitro == true)
        {
            if (nitroDuration >= nitroDurationInSeconds)
            {
                currentSpeed -= 0.02f * 5;
                playerCamera.transform.position += new Vector3(0, 0, 0.02f);
                if (currentSpeed <= speedBefoneNitro)
                {
                    currentSpeed = speedBefoneNitro;
                    canUseNitro = false;
                    nitroDuration = 0;
                    return;
                }
            }
            else
            {
                if (currentSpeed >= speedBefoneNitro + 10)
                {
                    currentSpeed = speedBefoneNitro + 10;
                }
                else
                {
                    playerCamera.transform.position -= new Vector3(0, 0, 0.02f);
                    currentSpeed += 0.02f * 5; // 0.02f * how much speed will increase per second
                }
                nitroDuration += 0.02f;
            }
        }
    }
    //------------------
    void ChangeLane(int direction)
    {
        int targetLine = currentLane + direction;
        if (targetLine < 0 || targetLine > 8)
            return;
        currentLane = targetLine;
        verticalTargetPosition = new Vector3((currentLane - 4), 0, 0);
    }
    void PlayerMovement()
    {

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

        if (playerIsDead == false)
        {
            rb.velocity = Vector3.forward * currentSpeed;

            transform.position = new Vector3(transform.position.x, 0.07f, transform.position.z);
            if (Input.GetKeyDown(KeyCode.A))//left
            {
                ChangeLane(-4);
            }
            if (Input.GetKeyDown(KeyCode.D))//right
            {
                ChangeLane(4);
            }
        }
        //transform.rotation = Quaternion.Euler(0, 180, 115);
        //gameObject.transform.Rotate(0, 0, rotateForce * Time.deltaTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        Obstacle obstacle = collision.transform.GetComponent<Obstacle>();
        if (obstacle)
        {
            playerIsDead = true;
        }
    }
    public void Right()
    {
        ChangeLane(4);
    }
    public void Left()
    {
        ChangeLane(-4);
    }
}