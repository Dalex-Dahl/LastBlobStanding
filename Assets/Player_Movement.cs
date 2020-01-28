using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rb;

    float horizontalMove;
    float verticalMove;

    public float airSpeed;
    public float jump;
    public float speed;

    Vector3 movement;
    public bool knockbackState;
    Vector3 knockbackDir;
    public float knockbackForce;
    public float knockup;
    bool isGrounded;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontalMove, 0, verticalMove).normalized;

        if (knockbackState == true || isGrounded == false)
        {
            rb.AddForce(new Vector3(movement.x * airSpeed, rb.velocity.y, movement.z * airSpeed));

            if ( isGrounded == true)
            {
                knockbackState = false;
            }

            return;
        }

        //This is a bit buggy and needs to be improved!
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity += new Vector3(0,jump,0);
        }


        Vector3 vel = movement * speed;
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hitzone")
        {
            knockbackState = true;
            knockbackDir = other.transform.position - transform.position;
            knockbackDir = -knockbackDir.normalized;
            rb.velocity = new Vector3(knockbackDir.x * knockbackForce, knockup, knockbackDir.z * knockbackForce);
            Debug.Log("Hit");
            isGrounded = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
