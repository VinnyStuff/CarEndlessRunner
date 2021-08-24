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
            SpawnScenery(scenaryRightSide, Random.Range(0, scenaryRightSide.Length), 11, false);

            SpawnScenery(scenaryLeftSide, Random.Range(0, scenaryLeftSide.Length), -11, true);
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
        if (scenaryIndex > instantiedScenary.Count - 1) //reset path through array
        {
            scenaryIndex = 0;
        }

        float distance = offset - (scenerySize * (numberOfscenery - 1));
        if (player.position.z > distance)
        {
            int canRecycle = Random.Range(0, 2);
            if (canRecycle == 0)
            {
                RecycleScenery(true);//is false
            }
            else if (canRecycle == 1)
            {
                RecycleScenery(true);
            }
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

                GameObject sceneryInstantied = Instantiate(currentArray[Random.Range(0, currentArray.Length)], new Vector3(positionX, 0, offset), transform.rotation);
                instantiedScenary[scenaryIndex] = sceneryInstantied;

                scenaryIndex += 1;

                positionX = -11;
                currentArray = scenaryLeftSide;
            }
            offset += scenerySize;
        }
        /*else //dps pensar nisso
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
        }*/
    }
}
