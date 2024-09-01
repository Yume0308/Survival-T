using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    // Movement properties
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    // Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Velocity
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        // set layer mask in terrain
        groundMask = LayerMask.GetMask("Terrain");
    }


    // Update is called once per frame
    void Update()
    {
        // checking if hit the ground to reset our falling velocity
        // otherwise we will keep falling or fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Resetting the velocity if we are grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Getting the input from the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Moving the player
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        // Checking if the player wants to jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Applying gravity
        velocity.y += gravity * Time.deltaTime;


        // Applying the velocity to the player
        characterController.Move(velocity * Time.deltaTime);
    }
}
