using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Agent : MonoBehaviour
{

    CharacterController controller;
    Animator animator;
    Vector3 movement;
    GameManager gm;

    const float MAX_DISTANCE = 2.5f;

    float verticalVelocity, 
        gravity = 12f,
        forcejump = 12f;

    int lane = 0; // -1, 0, 1

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.A))
        {
            // move to left
            MoveRight(false);
            animator.SetTrigger("Left");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // move to right
            MoveRight(true);
            animator.SetTrigger("Right");
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;

        if (lane == -1)
        {
            targetPosition += Vector3.left * MAX_DISTANCE;
        }
        else if (lane == 1)
        {
            targetPosition += Vector3.right * MAX_DISTANCE;
        }

        if (controller.isGrounded)
        {
            animator.SetBool("Grounded", true);
            verticalVelocity = -0.1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
                verticalVelocity = forcejump;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        movement.x = (targetPosition - transform.position).normalized.x * gm.agentSpeed;
        movement.y = verticalVelocity;
        movement.z = gm.agentSpeed;

        controller.Move(movement * Time.deltaTime);

    }

    void MoveRight(bool goingRight)
    {
        lane += (goingRight) ? 1 : -1;
        lane = Mathf.Clamp(lane, -1, 1);
    }

}