using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    //STREET SIZE = amount of coins in a street = not flexible , in moment is 13
    // street size = 60
    public GameObject coin;
    public List<GameObject> coinsInstantied;
    public int offset;
    public int offsetDestroy;
    public Transform player;
    public int coinCollumnsInStreet;
    void Start()
    {
        offsetDestroy = 30;
        offset = -25;
        coinCollumnsInStreet = 12;
        for (int i = 0; i < 3; i++)
        {
            SpawnCoins();
        }
    }

    void Update()
    {
        DestroyCoins();
    }

    public void SpawnCoins() //coins spawn in the street spawner
    {
        for (int i = 0; i < 12; i++)
        {
            if (Random.Range(0, 100) < 25) //line 1
            {
                GameObject coinInstantied = Instantiate(coin, new Vector3(-4, 0, offset), transform.rotation);
                coinsInstantied.Add(coinInstantied);
            }
            if (Random.Range(0, 100) < 25) //line 2
            {
                GameObject coinInstantied = Instantiate(coin, new Vector3(0, 0, offset), transform.rotation);
                coinsInstantied.Add(coinInstantied);
            }
            if (Random.Range(0, 100) < 25) //line 3
            {
                GameObject coinInstantied = Instantiate(coin, new Vector3(4, 0, offset), transform.rotation);
                coinsInstantied.Add(coinInstantied);
            }
            offset += 5;
        }
    }
    public void DestroyCoins()
    {
        for (int i = 0; i < coinsInstantied.Count; i++)
        {
            if (player.position.z - 10 > coinsInstantied[i].transform.position.z)
            {
                Destroy(coinsInstantied[i]);
                coinsInstantied.Remove(coinsInstantied[i]);
            }
        }
    }
}
