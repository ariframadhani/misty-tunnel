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

    const float MAX_LANE_DISTANCE = 2.5f;

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
        if (Input.GetKeyDown(KeyCode.A))
        {
            // MOVE TO LEFT
            MoveRight(false);
            animator.SetTrigger("Left");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // MOVE TO RIGHT
            MoveRight(true);
            animator.SetTrigger("Right");
        }

        // calculate where the agent should be in the future
        // z position still same with the current position
        Vector3 targetPosition = transform.position.z * Vector3.forward;

        // if one time action, lane = 0 ( -1 + 1 )
        if (lane == -1)
        {
            // if lane = -1 // target += -1 0 0 * 2.5 = -2.5 0 0
            targetPosition += Vector3.left * MAX_LANE_DISTANCE;
        }
        else if (lane == 1)
        {
            // if lane = -1 // target += 1 0 0 * 2.5 = 2.5 0 0
            targetPosition += Vector3.right * MAX_LANE_DISTANCE;
        }

        // ground check
        CheckingGrounded();

        movement = Vector3.zero;

        // x axis logic => where the agent should be - current position, then normalised by single meter on x axis * speed
        movement.x = (targetPosition - transform.position).normalized.x * gm.agentSpeed;
        movement.y = verticalVelocity;
        movement.z = gm.agentSpeed;

        controller.Move(movement * Time.deltaTime);
    }

    void CheckingGrounded()
    {
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
            // decresing each time (gravity)
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }

    void MoveRight(bool goingRight)
    {
        lane += (goingRight) ? 1 : -1;
        lane = Mathf.Clamp(lane, -1, 1);
    }

    void Death()
    {
        enabled = false;
        animator.SetTrigger("Death");
        gm.gameOver = true;
    }
}