using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightPuzzleReceiver : MonoBehaviour
{
    public bool isPowered = false;
    // Link the GateController.CheckPuzzleStatus() method here in the Inspector
    public UnityEvent onPowerGained; 

    // Optional: Link a Spawner here in the Inspector if an INCORRECT pulse hits this receiver
    // public SpawnEnemy enemySpawnerOnError; 

    public void GainPower()
    {
        if (!isPowered)
        {
            isPowered = true;
            onPowerGained.Invoke(); // Notify the main gate controller
            // enemySpawnerOnError.SpawnEnemiesNow(); // Example of triggering enemies on INCORRECT hit
        }
    }
}

