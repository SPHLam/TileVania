using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] GameObject bullet;
    GameObject gun;
    Vector2 deathVelocity = new Vector2(0f, 10f);
    float gravityScaleAtStart;
    bool isAlive = true;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody2D.gravityScale;

        gun = transform.GetChild(0).gameObject;
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

    void OnFire(InputValue inputValue)
    {
        if (!isAlive)
        {
            return;
        }
        if (inputValue.isPressed)
        {
            Instantiate(bullet, gun.transform.position, Quaternion.identity);
        }
    }

    void Run()
    {
        if (moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            myAnimator.SetBool("isRunning", true);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
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
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Death");
            myRigidBody2D.velocity = deathVelocity;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
