using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float bulletSpeed = 20f;
    PlayerMovement player;
    Quaternion playerDirection;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        playerDirection = player.transform.rotation;
    }

    void Update()
    {
        if (transform.rotation != playerDirection) // flipping the sprite and direction
        {
            bulletSpeed *= -1;
            transform.rotation = playerDirection;
        }
        myRigidBody.velocity = new Vector2(bulletSpeed, 0f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
