using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] float moveSpeed = 2f;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
    }

    private void Move()
    {
        if (moveSpeed > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }
}
