using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody2D;
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 50f;
    Vector2 deathVelocity = new Vector2(0f, 10f);
    float gravityScaleAtStart;
    bool isAlive = true;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody2D.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue inputValue)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue inputValue)
    {
        if (!isAlive)
        {
            return;
        }
        if (inputValue.isPressed)
        {
            if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myRigidBody2D.velocity += new Vector2(0f, jumpForce);
            }
        }
    }

    void Run()
    {
        if (moveInput.x > 0)
        {
            mySpriteRenderer.flipX = false;
            myAnimator.SetBool("isRunning", true);
        }
        else if (moveInput.x < 0)
        {
            mySpriteRenderer.flipX = true;
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;
    }

    void ClimbLadder()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (moveInput.y != 0)
            {
                myAnimator.SetBool("isClimbing", true);
            }
            else
            {
                myAnimator.SetBool("isClimbing", false);
            }
           
            myRigidBody2D.gravityScale = 0;
            Vector2 climbVelocity = new Vector2(myRigidBody2D.velocity.x, moveInput.y * moveSpeed);
            myRigidBody2D.velocity = climbVelocity;
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody2D.gravityScale = gravityScaleAtStart;
        }
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Death");
            myRigidBody2D.velocity = deathVelocity;
        }
    }
}
