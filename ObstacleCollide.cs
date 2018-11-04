using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollide : MonoBehaviour {

    GameObject respawn;
    GameManager gm;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
        respawn = GameObject.FindGameObjectWithTag("Respawn");
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.SendMessage("Death");
        }
    }

}
