using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    // Remove the public reference to ABShooting if only sr is needed
    // public ABShooting player; 
    private Rigidbody2D rb;
    public int damage;
    private SpriteRenderer sr;
    // Use a Coroutine for lifetime management instead of Update()
    public float lifeTime = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Find the player's SpriteRenderer once in Start
        SpriteRenderer playerSR = FindObjectOfType<ABShooting>().sr;
        sr.flipX = playerSR.flipX;

        // Set velocity once in Start(), not every frame in Update()
        if (sr.flipX)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        // Use Invoke to destroy the object after a set time
        Invoke("DestroyBullet", lifeTime);
    }

    // Update is now empty!
    void Update()
    {
        // Update is empty because movement and lifetime are handled in Start/Invoke
    }
    
    // New method for Invoke
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Use CompareTag for performance
        {
            // Ensure the other object actually has the component before trying to access it
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.EnemyTakeDamage(damage);
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Wall")) // Use CompareTag for performance
        {
            Destroy(gameObject);
        }
    }
}


