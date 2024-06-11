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

    [SerializeField] float moveSpeed = 5f;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Run();
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void Run()
    {
        if (moveInput.x > 0)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            mySpriteRenderer.flipX = true;
        }
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;
    }
}
