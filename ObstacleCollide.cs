using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollide : MonoBehaviour {

    GameObject respawn;

	// Use this for initialization
	void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //other.transform.position = respawn.transform.position;
            //other.gameObject.SendMessage("ResetCountMovement");
        }
    }
}
