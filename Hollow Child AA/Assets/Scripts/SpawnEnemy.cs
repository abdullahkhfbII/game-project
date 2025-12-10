using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public int enemiesToSpawn = 1; // How many enemies this spawner releases
    private bool hasSpawned = false; // Flag to ensure spawning only happens once per trigger event

    // Make sure the GameObject this script is attached to has a Collider2D component
    // marked as 'Is Trigger' in the Inspector.
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering object is the Player and we haven't spawned yet
        if (other.CompareTag("Player") && !hasSpawned)
        {
            SpawnEnemiesNow();
            hasSpawned = true; // Mark as spawned so it doesn't trigger again
        }
    }

    public void SpawnEnemiesNow()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        // Optional: Destroy the spawner object after spawning is complete
        // Destroy(gameObject, 0.1f); 
    }
}



