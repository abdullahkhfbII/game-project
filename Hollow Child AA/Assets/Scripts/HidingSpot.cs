using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    // A flag to quickly check if a player is currently occupying the spot
    // This can be checked by your AI scripts (e.g., in GhostEnemy.cs)
    public bool isOccupied = false;

    // This method is called automatically when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            isOccupied = true;
            Debug.Log("Player entered hiding spot.");
            // You can add audio feedback here, like a quiet breath sound
        }
    }

    // This method is called automatically when another collider exits the trigger collider attached to this GameObject
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            isOccupied = false;
            Debug.Log("Player exited hiding spot.");
            // You can add audio feedback here
        }
    }
}

