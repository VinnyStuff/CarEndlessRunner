using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetSpawner : MonoBehaviour
{
    public GameObject street; //60 is the street size
    public List<GameObject> instantiedStreet;
    public int streetIndex;
    public Transform player;
    public int offset;
    public int numberOfStreets;
    public int streetSize;
    // Start is called before the first frame update
    void Start()
    {
        streetSize = 60;
        numberOfStreets = 3;
        offset = 0;
        for (int i = 0; i < numberOfStreets; i++)
        {
            SpawnStreet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanRecycleStreet();
    }
    public void CheckCanRecycleStreet()
    {
        float distance = offset - (streetSize * (numberOfStreets - 1));
        if (player.position.z > distance)
        {
            RecycleStreet(instantiedStreet[streetIndex]);
        }
        if (streetIndex > instantiedStreet.Count - 1)
        {
            streetIndex = 0;
        }
    }
    public void SpawnStreet() // and recycle
    {
        GameObject streetnstantied = Instantiate(street, new Vector3(0, 0, offset), transform.rotation * Quaternion.Euler(0, 90, 0));
        instantiedStreet.Add(streetnstantied);
        offset += streetSize;
    }
    public void RecycleStreet(GameObject streetNewPosition)
    {
        streetNewPosition.transform.position = new Vector3(0, 0, offset);
        streetIndex += 1;
        offset += streetSize;
    }
}
