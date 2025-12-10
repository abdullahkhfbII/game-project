using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatPlatform : MonoBehaviour
{
    // Assign these empty GameObjects in the Inspector to define the boundaries
    public Transform PointA; // Leftmost point
    public Transform PointB; // Rightmost point
    public float Speed = 1f; // How fast the platform moves

    // We only need the starting positions, not the Transforms in the Update loop
    private Vector3 positionA;
    private Vector3 positionB;

    void Start()
    {
        // Store the exact Vector3 positions in Start()
        positionA = PointA.position;
        positionB = PointB.position;

        // Ensure the platform starts exactly on the A point
        transform.position = positionA;
    }
       
    void Update()
    {
        // PingPong automatically bounces a value between 0 and 1 over time
        float timeValue = Mathf.PingPong(Time.time * Speed, 1);

        // Lerp smoothly moves the position between A and B using the bouncing value (0=A, 1=B)
        transform.position = Vector3.Lerp(positionA, positionB, timeValue);
    }
    
    // Collision functions to keep the player attached
    void OnCollisionEnter2D (Collision2D Collision)
    {
        if(Collision.gameObject.CompareTag("Player"))
        {
            // Parent the player to the platform so they move together
            Collision.gameObject.transform.parent = this.transform;
        }
    }
    void OnCollisionExit2D (Collision2D Collision)
    {
        if(Collision.gameObject.CompareTag("Player"))
        {
            // Unparent the player when they jump off
            Collision.gameObject.transform.parent = null;
        }
    }
}
