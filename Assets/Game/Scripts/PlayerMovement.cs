using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    private float xInput;
    private float zInput;
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public float runBuildUp = 5f;
    public KeyCode runKey;
    private float speed;
    
    public float gravity = -20f;
    public float jumpHeight = 5f;
    private Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    public float crouchingSpeed = 2f;
    public float crouchBuidUp = 5f;
    public KeyCode crouchkey;
    private float charHeight;

    public AudioSource walkingSound;
    public AudioSource runningSound;

    void Start()
    {
        charHeight = controller.height;
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        CheckIfGrounded();
        CheckKeyPressed();
        UpdateMovement();
        UpdateAnimation();
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f || Input.GetKey(crouchkey))
        {
            velocity.y = -2f;
            if (controller.slopeLimit != 45f)
                controller.slopeLimit = 45f;
        }
    }

    private void CheckKeyPressed()
    {
        if (Input.GetKey(runKey))
        {
            speed = Mathf.Lerp(speed, runningSpeed, runBuildUp * Time.deltaTime);
            controller.height = Mathf.Lerp(controller.height, charHeight, 100f * Time.deltaTime);
        }
        else if (Input.GetKey(crouchkey))
        {
            speed = Mathf.Lerp(speed, crouchingSpeed, crouchBuidUp * Time.deltaTime);
            controller.height = Mathf.Lerp(controller.height, charHeight * 0.5f, 100f * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkingSpeed, runBuildUp * Time.deltaTime);
            controller.height = Mathf.Lerp(controller.height, charHeight, 100f * Time.deltaTime);
        }
    }

    private void UpdateMovement()
    {

        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 forwardMove = transform.forward * zInput;
        Vector3 rightMove = transform.right * xInput;
        controller.SimpleMove(Vector3.ClampMagnitude(forwardMove + rightMove, 1) * speed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            controller.slopeLimit = 90f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void UpdateAnimation()
    {
        if (xInput != 0 || zInput != 0)
        {
            if (Input.GetKey(runKey) == true)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);

                if (!runningSound.isPlaying)
                    runningSound.Play();
                walkingSound.Stop();
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);

                if (!walkingSound.isPlaying)
                    walkingSound.Play();
                runningSound.Stop();
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);

            walkingSound.Stop();
            runningSound.Stop();
        }
    }
}
