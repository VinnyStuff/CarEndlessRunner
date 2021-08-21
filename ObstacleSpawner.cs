using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //public float speedFactor;
    public GameObject[] obstacles;
    public GameObject player; //player position = 0
    public List<GameObject> obstaclesInstantied;
    public int emptinessBetweenObstacles;
    public int cameraView;
    public float currentPositionObstacle;
    private int xPositionObstacle;
    private int numberOfLines = 3;
    public int recycleCounter;
    public void Start()
    {
        recycleCounter = 0;
        emptinessBetweenObstacles = 20;
        cameraView = 10;
        for (int i = 0; i < numberOfLines; i++)
        {
            SpawnObstacles(currentPositionObstacle);
        }
    }
    public void Update()
    {
        DestroyTheObstacles();
    }
    public void DestroyTheObstacles()
    {
        for (int i = 0; i < obstaclesInstantied.Count; i++)
        {
            if (player.transform.position.z >= obstaclesInstantied[i].transform.position.z + cameraView)
            {
                Destroy(obstaclesInstantied[i]);
                obstaclesInstantied.Remove(obstaclesInstantied[i]);
                recycleCounter++;

                if (recycleCounter == 2)
                {
                    recycleCounter = 0;
                    SpawnObstacles(obstaclesInstantied[i].transform.position.z + emptinessBetweenObstacles);
                }
            }
        }
    }
    public void SpawnObstacles(float spawnPosition) //and recycle
    {
        for (int i = 0; i < 2; i++)
        {
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
            }
            else if (xPositionObstacle == 1)
            {
                PositionSpawnX = 0;
            }
            else if (xPositionObstacle == 2)
            {
                PositionSpawnX = 4;
            }
            GameObject newObstacle = Instantiate(obstacles[currentObstacle], new Vector3(PositionSpawnX, 2f, spawnPosition + emptinessBetweenObstacles), transform.rotation * Quaternion.Euler(0, 270, 0));
            newObstacle.AddComponent<Obstacle>();
            newObstacle.GetComponent<Obstacle>().speed = player.GetComponent<Player>().currentSpeed;
            obstaclesInstantied.Add(newObstacle);
        }
        currentPositionObstacle = currentPositionObstacle + emptinessBetweenObstacles;
    }
}
