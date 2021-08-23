using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    //scenery = Pieces of the scenery
    public GameObject[] scenaryTwoSides; // position x = 0
    public GameObject[] scenaryRightSide; // position x = 11
    public GameObject[] scenaryLeftSide; // position x = -11
    public List<GameObject> instantiedScenary;
    public int scenaryIndex;
    public Transform player;
    public int offset;
    public int numberOfscenery;
    public int scenerySize;
    public bool spawnOneSideScenery;
    // Start is called before the first frame update
    void Start()
    {
        scenerySize = 60;
        numberOfscenery = 3;
        offset = 0;
        SelectingTheFirstScenerysPieces();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanRecycleScenery();
    }
    public void SelectingTheFirstScenerysPieces()
    {
        for (int i = 0; i < 3; i++)
        {
            int selectCurrentScenery = Random.Range(0, 11);
            if (selectCurrentScenery >= 5)//spawn two sides
            {
                SpawnScenery(scenaryTwoSides, Random.Range(0, scenaryTwoSides.Length), 0, true);
            }
            else //spawn one side
            {
                SpawnScenery(scenaryRightSide, Random.Range(0, scenaryRightSide.Length), 11, false);

                SpawnScenery(scenaryLeftSide, Random.Range(0, scenaryLeftSide.Length), -11, true);
            }
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
            int canRecycle = Random.Range(0, 11);
            if (canRecycle >= 5)
            {
                RecycleScenery(instantiedScenary[scenaryIndex], false);
            }
            else
            {
                RecycleScenery(instantiedScenary[scenaryIndex], true);
            }
        }
        if (scenaryIndex > instantiedScenary.Count - 1) //reset path through array
        {
            scenaryIndex = 0;
        }
    }
    public void RecycleScenery(GameObject sceneryPiece, bool destroySceneryPiece)
    {
        if (destroySceneryPiece == true)
        {
            if (sceneryPiece.transform.position.x == 0)//is a two side
            {
                Destroy(sceneryPiece);
                instantiedScenary[scenaryIndex] = null;

                GameObject sceneryInstantied = Instantiate(scenaryTwoSides[Random.Range(0, scenaryTwoSides.Length)], new Vector3(0, 0, offset), transform.rotation);
                instantiedScenary[scenaryIndex] = sceneryInstantied;

                scenaryIndex += 1;
                offset += scenerySize;
            }
            else if (sceneryPiece.transform.position.x == 11)//is a right side
            {
                int positionX = 11;
                for (int i = 0; i < 2; i++)
                {
                    Destroy(instantiedScenary[scenaryIndex]);
                    instantiedScenary[scenaryIndex] = null;

                    GameObject sceneryInstantied = Instantiate(scenaryRightSide[Random.Range(0, scenaryRightSide.Length)], new Vector3(positionX, 0, offset), transform.rotation);
                    instantiedScenary[scenaryIndex] = sceneryInstantied;

                    scenaryIndex += 1;
                    positionX = -11;
                }
                offset += scenerySize;
            }
            else if (sceneryPiece.transform.position.x == -11)//is a left side
            {
                int positionX = 11;
                for (int i = 0; i < 2; i++)
                {
                    Destroy(instantiedScenary[scenaryIndex]);
                    instantiedScenary[scenaryIndex] = null;

                    GameObject sceneryInstantied = Instantiate(scenaryLeftSide[Random.Range(0, scenaryLeftSide.Length)], new Vector3(positionX, 0, offset), transform.rotation);
                    instantiedScenary[scenaryIndex] = sceneryInstantied;

                    scenaryIndex += 1;
                    positionX = -11;
                }
                offset += scenerySize;
            }
        }
        else
        {
            if (sceneryPiece.transform.position.x == 0)//is a two side
            {
                sceneryPiece.transform.position = new Vector3(0, 0, offset);
                scenaryIndex += 1;
                offset += scenerySize;
            }
            else if (sceneryPiece.transform.position.x == 11)//is a right side
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
            else if (sceneryPiece.transform.position.x == -11)//is a left side
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
    }
}
