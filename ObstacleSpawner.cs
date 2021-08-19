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
    public int currentPositionObstacle;
    private int xPositionObstacle;
    private int numberOfLines = 8;

    public void Start()
    {
        //speedFactor = 1;
        emptinessBetweenObstacles = 20;
        for (int i = 0; i < numberOfLines; i++)
        {
            SpawnObstacles();
        }
    }
    public void Update()
    {
        SpawnObstacles();
        DestroyTheObstacles();
    }
    public void DestroyTheObstacles()
    {
        for (int i = 0; i < obstaclesInstantied.Count; i++)
        {
            if (player.transform.position.z >= obstaclesInstantied[i].transform.position.z + emptinessBetweenObstacles)
            {
                Destroy(obstaclesInstantied[i]);
                obstaclesInstantied.Remove(obstaclesInstantied[i]);
            }
        }
    }
    public void SpawnObstacles() //and recycle
    {
        if (player.transform.position.z >= currentPositionObstacle - (numberOfLines * numberOfLines - 1))
        {
            for (int i = 0; i < 2; i++)
            {
                int currentObstacle = Random.Range(0, obstacles.Length - 1);
                int xPositionObstacleChoice = Random.Range(0, 3);
                if (xPositionObstacleChoice == xPositionObstacle)
                {
                    while (xPositionObstacleChoice != xPositionObstacle)
                    {
                        xPositionObstacleChoice = Random.Range(0, 3);
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

                GameObject newObstacle = Instantiate(obstacles[currentObstacle], new Vector3(PositionSpawnX, 0, currentPositionObstacle + emptinessBetweenObstacles), transform.rotation);
                obstaclesInstantied.Add(newObstacle);
            }
            currentPositionObstacle = currentPositionObstacle + emptinessBetweenObstacles;
        }
    }
}
