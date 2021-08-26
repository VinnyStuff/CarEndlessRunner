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
    //rotate player
    public GameObject playerFront;
    public GameObject playerBack;
    public bool canRotateRight;
    public bool canRotateLeft;
    //NITRO
    public float speedBefoneNitro;
    public float nitroDurationInSeconds;
    public float nitroDuration;
    public bool canUseNitro;
    public Camera playerCamera;
    public Vector3 playerCameraStartPosition;
    void Start()
    {
        backRight = false;
        canRotateRight = false;
        canRotateLeft = false;
        playerCameraStartPosition = playerCamera.transform.localPosition;
        nitroDuration = 0;
        SelectCar();
        rb = GetComponent<Rigidbody>();
        playerIsDead = false;
        currentSpeed = speed;
    }
    private void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.W))
        {
            canUseNitro = true;
            speedBefoneNitro = currentSpeed;
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
                CanRotateThePlayer("left");
            }
            if (Input.GetKeyDown(KeyCode.D))//right
            {
                ChangeLane(4);
                CanRotateThePlayer("right");
            }
            RotateThePlayer();
        }
        //transform.rotation = Quaternion.Euler(0, 180, 115);
        //gameObject.
    }
    public void OnCollisionEnter(Collision collision)
    {
        Obstacle obstacle = collision.transform.GetComponent<Obstacle>();
        if (obstacle)
        {
            playerIsDead = true;
        }
    }
    public void CanRotateThePlayer(string direction)
    {
        if (direction == "right")
        {
            canRotateRight = true;
        }
        if (direction == "left")
        {
            canRotateLeft = true;
        }
    }
    public bool backRight;
    public bool backLeft;
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }
    public void RotateThePlayer()
    {
        if (canRotateRight == true)
        {
            if (playerBack.transform.eulerAngles.y >= 15 && backRight == false)
            {
                backRight = true;
            }
            else if (playerBack.transform.eulerAngles.y <= 30 && backRight == false)
            {
                playerBack.transform.Rotate(0, 50 * Time.deltaTime, 0);
            }
            if (backRight == true)
            {
                playerBack.transform.Rotate(0, -50 * Time.deltaTime, 0);
                if (WrapAngle(playerBack.transform.eulerAngles.y) <= 0)
                {
                    playerBack.transform.rotation = Quaternion.Euler(0, 0, 0);
                    canRotateRight = false;
                    backRight = false;
                }
            }
        }
        else if (canRotateLeft == true)
        {
            if (WrapAngle(playerBack.transform.eulerAngles.y) <= -15 && backLeft == false)
            {
                backLeft = true;
            }
            else if (WrapAngle(playerBack.transform.eulerAngles.y) >= -30 && backLeft == false)
            {
                playerBack.transform.Rotate(0, -50 * Time.deltaTime, 0);
            }
            if (backLeft == true)
            {
                playerBack.transform.Rotate(0, 50 * Time.deltaTime, 0);
                if (WrapAngle(playerBack.transform.eulerAngles.y) >= 0)
                {
                    playerBack.transform.rotation = Quaternion.Euler(0, 0, 0);
                    canRotateLeft = false;
                    backLeft = false;
                }
            }
        }
    }
}