using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]
public class Agent : MonoBehaviour {

    CharacterController controller;
    NeuronNetwork brain;
    GameObject[] obstacles;
    GameObject respawn;
    Vector3 movement, startPosition, goingTo;
    Animator animator;

    float gravity = 20f,
        transitionSpeed = 10f,
        verticalVel = 0f,
        jumpVel = 23f,
        score = 0,
        movementSpeed = 7f;

    int movementCount;
    float[] inputs;

    bool jump;

	// Use this for initialization
	void Start () {
        brain = new NeuronNetwork(3, 6, 5);
        controller = GetComponent<CharacterController>();
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        movementCount = 0;
        score = 0;
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update ()
    {
        movement = Vector3.zero;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        CharacterGrativity();
        MovementController();
        MovementTransition();

        movement.y = verticalVel * Time.deltaTime;
        //movement.z = movementSpeed;

        Debug.Log(gravity);
        Debug.Log(jumpVel);
        controller.Move(movement * Time.deltaTime);
	}

    void MovementTransition()
    {
        if (movementCount <= -1)
        {
            movementCount = -1;
            GoingToMovement(-2f);

        }
        else if (movementCount >= 1)
        {
            movementCount = 1;
            GoingToMovement(2f);
        }
        else
        {
            movementCount = 0;
            GoingToMovement(0f);
        }
    }

    /**
     * Going to movement on x axis
     * */
    void GoingToMovement(float x)
    {
        Vector3 goingTo = new Vector3(x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(startPosition, goingTo, transitionSpeed * Time.deltaTime);
    }
    
    void MovementController()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            movementCount -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movementCount += 1;
        }
        else if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            verticalVel = Mathf.Pow(jumpVel, 2);
        }

    }
    
    void CharacterGrativity()
    {
        if (controller.isGrounded)
        {
            verticalVel = -0.5f;
        }
        else
        {
            verticalVel -= gravity;
        }
    }

    void ResetCountMovement()
    {
        movementCount = 0;
    }
    
}
