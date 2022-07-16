using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    private CharacterController characterController;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;

    private Vector3 velocity;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 vector3 = transform.right * x + transform.forward * y;

        characterController.Move(vector3 * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
