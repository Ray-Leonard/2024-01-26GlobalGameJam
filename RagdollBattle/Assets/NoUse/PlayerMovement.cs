using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Config")]
    [SerializeField] private float moveSpeed = 5f;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField] private float jumpForce = 5f;
    public float JumpForce { get => jumpForce; set => jumpForce = value; }


    [SerializeField] private LayerMask groundLayer;
    
    private bool isGrounded;
    public bool IsGrounded { get => isGrounded; }


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        /// MOVE
        // this need to adapt to multiplayer input and using new input system. 
        float moveInput = Input.GetAxis("Horizontal");   
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        /// JUMP
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        // adjust player local scale
        if(moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((groundLayer & 1 << collision.gameObject.layer) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((groundLayer & 1 << collision.gameObject.layer) != 0)
        {
            isGrounded = false;
        }
    }
}
