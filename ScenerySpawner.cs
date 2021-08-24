using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    //scenery = Pieces of the scenery // always starts with the right
    public GameObject[] scenaryRightSide; // position x = 11
    public GameObject[] scenaryLeftSide; // position x = -11
    public List<GameObject> instantiedScenary;
    public int scenaryIndex;
    public Transform player;
    public int offset;
    public int numberOfscenery;
    public int scenerySize;
    public string[] biomes;
    public bool changingBiome;//change the biome
    public string currentBiome;
    public int numberOfBiomes;
    public int valueMinBiomeScenery;
    public int valueMaxBiomeScenery;
    // Start is called before the first frame update
    void Start()
    {
        changingBiome = false;
        numberOfBiomes = biomes.Length;
        scenerySize = 60;
        numberOfscenery = 3;
        offset = 0;
        SelectingTheFirstScenerysPieces();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanRecycleScenery();
        int selectCurrentBiome = Random.Range(0, numberOfBiomes);
    }
    public void SelectingTheFirstScenerysPieces()
    {
        int selectCurrentBiome = Random.Range(0, numberOfBiomes);
        currentBiome = biomes[selectCurrentBiome];

        if (currentBiome == "desert")
        {
            valueMinBiomeScenery = 0;
            valueMaxBiomeScenery = 3;
        }
        else if (currentBiome == "florest")
        {
            valueMinBiomeScenery = 3;
            valueMaxBiomeScenery = 6;
        }        
        else if (currentBiome == "town")
        {
            valueMinBiomeScenery = 6;
            valueMaxBiomeScenery = 9;
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnScenery(scenaryRightSide, Random.Range(valueMinBiomeScenery, valueMaxBiomeScenery), 11, false);

            SpawnScenery(scenaryLeftSide, Random.Range(valueMinBiomeScenery, valueMaxBiomeScenery), -11, true);
        }
    }
    public void SpawnScenery(GameObject[] currentList,int scenerySelect, int positionX, bool canChangeOffset)
    {
        GameObject sceneryInstantied = Instantiate(currentList[scenerySelect], new Vector3(positionX, 0, offset), transform.rotation);
        instantiedScenary.Add(sceneryInstantied);
        if (canChangeOffset == true)
        {
            offset += scenerySize;
        }
    }
    public void CheckCanRecycleScenery()
    {
        float distance = offset - (scenerySize * (numberOfscenery - 1));
        if (player.position.z > distance)
        {
            int canRecycle = Random.Range(0, 2);
            if (canRecycle == 0)
            {
                RecycleScenery(true);
            }
            else if (canRecycle == 1)
            {
                if (changingBiome == false)
                {
                    RecycleScenery(false);
                }
                else
                {
                    RecycleScenery(true);
                }
            }
            if (scenaryIndex > instantiedScenary.Count - 1) //reset path through array
            {
                scenaryIndex = 0;
            }
            if (changingBiome == true)
            {
                for (int i = 0; i < instantiedScenary.Count - 1; i++)
                {
                    if (!instantiedScenary[i].name.Contains(currentBiome))
                    {
                        return;
                    }
                }
                changingBiome = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadNextBiome();
        }
    }
    public void RecycleScenery(bool destroySceneryPiece)
    {
        if (destroySceneryPiece == true)
        {
            int positionX = 11;
            GameObject[] currentArray = scenaryRightSide;
            for (int i = 0; i < 2; i++)
            {
                Destroy(instantiedScenary[scenaryIndex]);
                instantiedScenary[scenaryIndex] = null;

                GameObject sceneryInstantied = Instantiate(currentArray[Random.Range(valueMinBiomeScenery, valueMaxBiomeScenery)], new Vector3(positionX, 0, offset), transform.rotation);
                instantiedScenary[scenaryIndex] = sceneryInstantied;

                scenaryIndex += 1;

                positionX = -11;
                currentArray = scenaryLeftSide;
            }
            offset += scenerySize;
        }
        else
        {
            int positionX = 11;
            for (int i = 0; i < 2; i++)
            {
                instantiedScenary[scenaryIndex].transform.position = new Vector3(positionX, 0, offset);
                scenaryIndex += 1;
                positionX = -11;
            }
            offset += scenerySize;
        }
    }
    public void LoadNextBiome()
    {
        if (currentBiome == "desert")
        {
            currentBiome = biomes[1];
        }
        else if (currentBiome == "florest")
        {
            currentBiome = biomes[2];
        }
        else if (currentBiome == "town")
        {
            currentBiome = biomes[0];
        }

        changingBiome = true;

        if (currentBiome == "desert")
        {
            valueMinBiomeScenery = 0;
            valueMaxBiomeScenery = 3;
        }
        else if (currentBiome == "florest")
        {
            valueMinBiomeScenery = 3;
            valueMaxBiomeScenery = 6;
        }
        else if (currentBiome == "town")
        {
            valueMinBiomeScenery = 6;
            valueMaxBiomeScenery = 9;
        }
    }
}
