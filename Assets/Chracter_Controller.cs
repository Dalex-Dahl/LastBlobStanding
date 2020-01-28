using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chracter_Controller : MonoBehaviour
{
    CharacterController characterController;

    float horizontalMove;
    float verticalMove;
    float gravity = 20f;

    public int speed;

    public int jumpheight;

    Vector3 movement;


    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.isGrounded)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
            movement =  new Vector3(horizontalMove, 0, verticalMove).normalized;
            movement *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.y = jumpheight;
            }
        }

        movement.y -= gravity * Time.deltaTime;
        characterController.Move(movement * Time.deltaTime);
    }
}
