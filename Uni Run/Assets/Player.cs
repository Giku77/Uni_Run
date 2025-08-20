using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 1f; 
    private int jumpCount = 0; // Track the number of jumps
    private const int maxJumps = 2; // Allow double jump

    private Animator animator;
    private Rigidbody2D rb2d;

    private GameManager gameManager;

    private bool isGrounded = true;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Please ensure it is tagged correctly.");
        }
        
        jumpCount = 0;
        isGrounded = true;
        isDead = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.AddScore(10);
        }

        if (isDead)
        {
            return; 
        }
        if (Input.GetMouseButtonDown(0) && jumpCount < maxJumps)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            ++jumpCount;
            Debug.Log("Jump Count: " + jumpCount);
            //rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        animator.SetBool("Grounded", isGrounded);
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //rb2d.linearVelocity = movement * 5f; // Adjust speed as necessary
        //if (movement != Vector2.zero)
        //{
        //    animator.SetFloat("MoveX", moveHorizontal);
        //    animator.SetFloat("MoveY", moveVertical);
        //    animator.SetBool("IsMoving", true);
        //}
        //else
        //{
        //    animator.SetBool("IsMoving", false);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collided with Ground");
            jumpCount = 0;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead && other.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");
        rb2d.bodyType = RigidbodyType2D.Kinematic; // Stop the player from moving
        rb2d.linearVelocity = Vector2.zero; // Reset velocity to stop movement
        gameManager.OnPlayerDead();
    }

}
