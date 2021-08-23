using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    //scenery = Pieces of the scenery
    public GameObject[] scenary;
    public List<GameObject> instantiedScenary;
    public int scenaryIndex;
    public Transform player;
    public int offset;
    public int numberOfscenery;
    public int ScenerySize;
    // Start is called before the first frame update
    void Start()
    {
        ScenerySize = 60;
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
            int scenerySelect = Random.Range(0, scenary.Length);
            SpawnScenery(scenerySelect);
        }
    }
    public void SpawnScenery(int scenerySelect)
    {
        GameObject streetnstantied = Instantiate(scenary[scenerySelect], new Vector3(11, 0, offset), transform.rotation);
        instantiedScenary.Add(streetnstantied);
        offset += ScenerySize;
    }
    public void CheckCanRecycleScenery()
    {
        float distance = offset - (ScenerySize * (numberOfscenery - 1));
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
            Destroy(sceneryPiece);
            instantiedScenary[scenaryIndex] = null;

            GameObject streetinstantied = Instantiate(scenary[Random.Range(0, scenary.Length)], new Vector3(11, 0, offset), transform.rotation);
            instantiedScenary[scenaryIndex] = streetinstantied;

            scenaryIndex += 1;
            offset += ScenerySize;
        }
        else
        {
            sceneryPiece.transform.position = new Vector3(11, 0, offset);
            scenaryIndex += 1;
            offset += ScenerySize;
        }
    }
}
