using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnManager : MonoBehaviour {

    public GameObject[] grounds;
    List<GameObject> groundCollection;
    Transform agent;
    
    float spawnZ = 0f;
    float groundLength = 100f;
    float safezone = 100;
    int spawnScreen = 5;
    int randomIndex;
    int lastIndex = 0;

	// Use this for initialization
	void Start () {
        agent = GameObject.FindGameObjectWithTag("Player").transform;
        groundCollection = new List<GameObject>();
        
        for(int i = 0; i < spawnScreen; i++)
        {
            if (i < 1)
                SpawnGround(0);
            else
                SpawnGround(1);
        }
    }
    
    // Update is called once per frame
    void Update () {
        
        if (agent.position.z - safezone > (spawnZ - spawnScreen * groundLength))
        {
            SpawnGround(1);
            DeleteGround();
        }
    }

    void SpawnGround(int index = -1)
    {
        GameObject go;
        
        go = Instantiate(grounds[index]) as GameObject;
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        
        spawnZ += groundLength;

        // add to ground collection to delete ground gameobjects
        groundCollection.Add(go);
    }

    void DeleteGround()
    {
        Destroy(groundCollection[0]);
        groundCollection.RemoveAt(0);
    }
    
}
