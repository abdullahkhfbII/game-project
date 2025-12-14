using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDebris : MonoBehaviour
{

    [Header("Movement Settings")]
    public bool isMovable = true;           // Can be pushed by the player
    public float pushForce = 2f;            // Force applied when player touches
    public bool floatInAir = true;          // Should it hover/floating?
    public float floatAmplitude = 0.5f;     // Vertical floating distance
    public float floatFrequency = 1f;       // Floating speed

    private Rigidbody2D rb;
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        if (floatInAir)
        {
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (floatInAir)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isMovable) return;

        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Apply horizontal push based on player movement
                Vector2 pushDir = new Vector2(playerRb.velocity.x, 0);
                rb.AddForce(pushDir * pushForce, ForceMode2D.Force);
            }
        }
    }
}
