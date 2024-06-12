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
    BoxCollider2D myCollider;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 50f;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        ClimbLadder();
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 climbVelocity = new Vector2(myRigidBody2D.velocity.x, moveInput.y * moveSpeed);
            myRigidBody2D.velocity = climbVelocity;
        }
    }
}
