using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    
    GameObject agent;
    Vector3 startPosition, agentPosition;

	// Use this for initialization
	void Start () {
        agent = GameObject.FindGameObjectWithTag("Player");
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
	}
	
	// Update is called once per frame
	void Update () {
        //agentPosition = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z);
        
        transform.position = startPosition + agentPosition;
    }
}
