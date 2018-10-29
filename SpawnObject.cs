using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnObject : MonoBehaviour
{

    [System.Serializable]
    public class GOSpawn
    {
        // if first variable is string, it'll become main title, else just element count
        public string name;
        public GameObject item;
        public bool special;
        
    }

    public List<GOSpawn> GOSpawns = new List<GOSpawn>();

    GameManager gm;
    int chilidCount, randomIndexObject, randomChild;

    // Use this for initialization
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        chilidCount = transform.childCount;
        GameObject go;

        int max_spawn_special = 0;
        int max_spawn = 2;
        
        for (int i = 0; i < GOSpawns.Count; i++)
        {
            for (int j = 0; j < Random(1, gm.spawnPortion); j++)
            {
                // random spawn index object
                randomIndexObject = Random(0, GOSpawns.Count);

                // random child on spawn point
                randomChild = Random(0, chilidCount);

                // spawn special object on spesific level
                if (gm.level < 2)
                {
                    while (GOSpawns[randomIndexObject].special)
                    {
                        randomIndexObject = Random(0, GOSpawns.Count);
                    }
                }


                //avoid start special object from starting ground and final fround
                if (validPositionSpecial(randomChild))
                {
                    while (GOSpawns[randomIndexObject].special)
                    {
                        randomIndexObject = Random(0, GOSpawns.Count);
                    }
                }

                // set max spawn on special object
                if (GOSpawns[randomIndexObject].special)
                {
                    max_spawn_special++;
                }

                // validate max spawn special
                if (max_spawn_special >= max_spawn)
                {
                    max_spawn_special = max_spawn;
                    while (GOSpawns[randomIndexObject].special)
                    {
                        randomIndexObject = Random(0, GOSpawns.Count);

                        max_spawn_special = 0;
                    }
                }

                // instantiate ( spawning object )
                go = Instantiate(GOSpawns[randomIndexObject].item);

                while (transform.GetChild(randomChild).childCount == 1)
                {
                    randomChild = Random(0, chilidCount);
                }

                // set object on parent with applying parent position
                go.transform.SetParent(transform.GetChild(randomChild).transform);
                go.transform.position = transform.GetChild(randomChild).transform.position;
            }
        }
    }



    // avoid start object from starting ground and final fround
    bool validPositionSpecial( int randomChild )
    {
        if(randomChild == 9 || randomChild == 10 || randomChild == 11 || 
            randomChild == 21 || randomChild == 22 || randomChild == 23)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    int Random(int one, int two)
    {
        return UnityEngine.Random.Range(one, two);
    }

    float Random(float one, float two)
    {
        return UnityEngine.Random.Range(one, two);
    }


}
