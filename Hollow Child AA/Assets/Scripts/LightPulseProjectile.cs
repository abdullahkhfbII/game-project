using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulseProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f; // Destroy after a few seconds if it misses
    
    // Add a public variable to hold the reference to the LightPuzzleReceiver script
    // This allows the receiver to detect the projectile's collision
    // private LightPuzzleReceiver receiverScript; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Projectile needs a Rigidbody2D!");
            return;
        }
        
        // Destroy the object after 'lifetime' seconds to prevent infinite travel
        Destroy(gameObject, lifetime);
    }
    
    // Use a custom method to set the direction when spawned by the PlayerController
    public void Launch(Vector2 direction)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    // This handles the collision when the 'Is Trigger' collider hits something
    void OnTriggerEnter2D(Collider2D other)
    {
        LightPuzzleReceiver receiver = other.GetComponent<LightPuzzleReceiver>();
        if (receiver != null)
        {
            receiver.GainPower();
            Destroy(gameObject); // Destroy the pulse on impact with a receiver
        }
        // You can add logic here for walls or other obstacles that should stop the pulse
        // e.g. else if (other.CompareTag("Wall")) { Destroy(gameObject); }
    }
}

