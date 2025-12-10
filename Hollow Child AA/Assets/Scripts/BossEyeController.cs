using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeController : MonoBehaviour
{
    public int bossHealth = 10;
    public SpawnEnemy miniShadowSpawner; // Assign the spawner GameObject here

    public void TakeDamageFromLightPulse()
    {
        // This is called by PlayerController.ActivateLightPulse()
        bossHealth -= 1;
        Debug.Log("Boss Health: " + bossHealth);

        if (bossHealth <= 0)
        {
            DefeatBoss();
        }
        else
        {
            // Trigger the swarm phase after taking damage
            StartCoroutine(SwarmPhase());
        }
    }

    IEnumerator SwarmPhase()
    {
        // Play an alarm sound
        miniShadowSpawner.SpawnEnemiesNow();
        yield return new WaitForSeconds(5f); // Wait for the player to clear the enemies
        // Resume normal boss attacks here
    }

    void DefeatBoss()
    {
        Debug.Log("Boss Defeated! Level 1 Complete.");
        // Play explosion effects/cutscene
        // Trigger Level Achievement logic here (e.g., scene transition)
        Destroy(gameObject);
    }
    
    // Add other boss attack patterns (Darkness beams, Observation mode) here as methods
}

