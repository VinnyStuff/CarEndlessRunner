using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public List<GameObject> obstaclesInstantied;
    public GameObject player; //player position = 0
    public GameObject money;
    public List<GameObject> moneyInstantied;
    public int emptinessBetweenObstacles;
    public int cameraView;
    public float currentPositionObstacle;
    private int xPositionObstacle;
    public int numberOfLines;
    public int destroyIndex;
    public void Start()
    {
        numberOfLines = 3;
        destroyIndex = 0;
        emptinessBetweenObstacles = 20;
        cameraView = 10;
        for (int i = 0; i < numberOfLines; i++)
        {
            SpawnObstacles(currentPositionObstacle);
        }
    }
    public void FixedUpdate()
    {
        DestroyTheObstacles();
    }
    public void SpawnObstacles(float spawnPosition) //and money
    {
        bool haveCarinLine1 = false;
        bool haveCarinLine2 = false; 
        bool haveCarinLine3 = false;
        for (int i = 0; i < 2; i++)
        {
            int carVariationZ = Random.Range(-8, 9);
            int currentObstacle = Random.Range(0, obstacles.Length - 1);
            int xPositionObstacleChoice = Random.Range(0, 3);
            if (xPositionObstacleChoice == xPositionObstacle)
            {
                while (true)
                {
                    xPositionObstacleChoice = Random.Range(0, 3);
                    if (xPositionObstacleChoice != xPositionObstacle)
                    {
                        break;
                    }
                }
            }
            xPositionObstacle = xPositionObstacleChoice;
            int PositionSpawnX = 0;
            if (xPositionObstacle == 0)
            {
                PositionSpawnX = -4;
                haveCarinLine1 = true;
            }
            else if (xPositionObstacle == 1)
            {
                PositionSpawnX = 0;
                haveCarinLine2 = true;
            }
            else if (xPositionObstacle == 2)
            {
                PositionSpawnX = 4;
                haveCarinLine3 = true;
            }
            GameObject newObstacle = Instantiate(obstacles[currentObstacle], new Vector3(PositionSpawnX, 0, (spawnPosition + emptinessBetweenObstacles) + (carVariationZ)), transform.rotation * Quaternion.Euler(0, 270, 0));
            newObstacle.AddComponent<Obstacle>();
            obstaclesInstantied.Add(newObstacle);
        }
        //Spawn Money
        if (haveCarinLine1 == false)
        {
            moneyManagement(-4, spawnPosition + emptinessBetweenObstacles);
        }
        else if (haveCarinLine2 == false)
        {
            moneyManagement(0, spawnPosition + emptinessBetweenObstacles);
        }
        else if (haveCarinLine3 == false)
        {
            moneyManagement(4, spawnPosition + emptinessBetweenObstacles);
        }
        //Spawn money
        currentPositionObstacle = currentPositionObstacle + emptinessBetweenObstacles;
    }
    public void DestroyTheObstacles()
    {
        for (int i = 0; i < obstaclesInstantied.Count; i++)
        {
            if (player.transform.position.z >= obstaclesInstantied[i].transform.position.z + cameraView)
            {
                Destroy(obstaclesInstantied[i]);
                obstaclesInstantied.Remove(obstaclesInstantied[i]);
                destroyIndex++;

                if (destroyIndex >= 2) // spawn new obstacles
                {
                    destroyIndex = 0;
                    float maxPosition = GetThePositionOffLatestObstacle();
                    SpawnObstacles(maxPosition);
                }
            }
        }
    }
    public float GetThePositionOffLatestObstacle()
    {
        float maxPosition = 0;
        for (int i = 0; i < obstaclesInstantied.Count; i++)
        {
            if (obstaclesInstantied[i].transform.position.z > maxPosition)
            {
                maxPosition = obstaclesInstantied[i].transform.position.z;
            }
        }
        return maxPosition;
    }
    public void moneyManagement(int x_position, float z_position) //after put can destroy
    {
        GameObject newMoney = Instantiate(money, new Vector3(x_position, 0, z_position), transform.rotation);
        moneyInstantied.Add(newMoney);
    }
}
