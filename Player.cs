using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public HUD hud;
    public CollectableSpawner collectableSpawner;

    public float speed;// = 15f;
    public float currentSpeed;
    private Rigidbody rb;
    public float laneWidth = 4.0f;

    // car movement
    public float steering = 1.0f;
    public float targetDistance = 10f;
    public GameObject target;
    private Vector3 sidewaysVelocity = Vector3.zero;

    public bool isPlayerDead;
    public List<GameObject> cars;
    public float velocityIncreasingPerSecond;

    // NITRO
    public float speedBefoneNitro;
    public float nitroDurationInSeconds;
    public float nitroDuration;
    public bool canUseNitro;
    public Camera playerCamera;
    public Vector3 playerCameraStartPosition;
    void Start()
    {
        playerCameraStartPosition = playerCamera.transform.localPosition;
        nitroDuration = 0;
        SelectCar();
        rb = GetComponent<Rigidbody>();
        isPlayerDead = false;
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

    private float virtualCameraX;
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
        if (canUseNitro == false && isPlayerDead == false)
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
    float getCurrentLane()
    {
        return target.transform.position.x;
    }
    void ChangeLaneBy(int lane)
    {
        float currentX = getCurrentLane();
        target.transform.position = new Vector3(currentX + (laneWidth * lane), target.transform.position.y, target.transform.position.z);
    }
    void PlayerMovement()
    {

        if (!isPlayerDead)
        {
            float currentX = getCurrentLane();
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentX > -4) ChangeLaneBy(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentX < 4) ChangeLaneBy(1);
            }

            // make ball go forward with the car
            target.transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z + targetDistance);

            // move car sideways
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref sidewaysVelocity, (1 / steering) * 0.3f * (50 / currentSpeed * 0.5f));

            // rotate car
            var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * steering * 3.0f * (50 / currentSpeed * 0.5f));

            transform.position = new Vector3(transform.position.x, 0.07f, transform.position.z);
            rb.velocity = Vector3.forward * currentSpeed; // make physics engine happy
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        Obstacle obstacle = collision.transform.GetComponent<Obstacle>();
        if (obstacle)
        {
            isPlayerDead = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {      
        Collectable collectable = other.GetComponent<Collectable>();
        if (collectable)
        {
            if (collectable.coin)
            {
                hud.IncreaseCoins();
                other.gameObject.SetActive(false);
            }
            else if (collectable.ghost)
            {
                Debug.Log("GHOST MODE");
            }
            else if (collectable.nitro)
            {
                Debug.Log("NITRO");
            }
        }
    }
}