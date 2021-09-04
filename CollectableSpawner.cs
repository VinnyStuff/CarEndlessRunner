using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    //STREET SIZE = amount of coins in a street = not flexible , in moment is 13
    // street size = 60
    public GameObject coin;
    public GameObject[] collectable; 
    public List<GameObject> collectableInstantied;
    public int offset;
    public int offsetDestroy;
    public Transform player;
    public int coinCollumnsInStreet;
    public int fixedIndex;
    void Start()
    {
        offsetDestroy = 30;
        offset = -25;
        coinCollumnsInStreet = 12;
        for (int i = 0; i < 3; i++)
        {
            SpawnCollectable();
        }
    }
    private void FixedUpdate()
    {
        fixedIndex += 1;
        if (fixedIndex >= 50)
        {
            DestroyColletacle();
            fixedIndex = 0;
        }
    }
    public void SpawnCollectable() //coins spawn in the street spawner
    {
        for (int i = 0; i < coinCollumnsInStreet; i++)
        {
            int CollectableChoice = Random.Range(0, 10);
            if (CollectableChoice == 1)//spawn a collectable
            {
                Collectable();
            }
            else //spawn coins
            {
                Coin();
            }
        }
    }
    public void DestroyColletacle()
    {
        for (int i = collectableInstantied.Count - 1; i > 0; i--)
        {
            if (player.position.z - 10 > collectableInstantied[i].transform.position.z)
            {
                Destroy(collectableInstantied[i]);
                collectableInstantied.Remove(collectableInstantied[i]);
            }
        }
    }
    public void Coin()
    {
        if (Random.Range(0, 100) < 20) //line 1
        {
            GameObject coinInstantied = Instantiate(coin, new Vector3(-4, 0, offset), transform.rotation);
            collectableInstantied.Add(coinInstantied);
        }
        if (Random.Range(0, 100) < 20) //line 2
        {
            GameObject coinInstantied = Instantiate(coin, new Vector3(0, 0, offset), transform.rotation);
            collectableInstantied.Add(coinInstantied);
        }
        if (Random.Range(0, 100) < 20) //line 3
        {
            GameObject coinInstantied = Instantiate(coin, new Vector3(4, 0, offset), transform.rotation);
            collectableInstantied.Add(coinInstantied);
        }
        offset += 5;
    }
    public void Collectable()
    {
        int collectableChoice = Random.Range(0, collectable.Length);
        int[] possiblePosition = { -4, 0, 4 };
        int positionChoice = Random.Range(0, possiblePosition.Length);

        GameObject newCollectable = Instantiate(collectable[collectableChoice], new Vector3(possiblePosition[positionChoice], 0, offset), transform.rotation);
        collectableInstantied.Add(newCollectable);
        offset += 5;
    }
}
