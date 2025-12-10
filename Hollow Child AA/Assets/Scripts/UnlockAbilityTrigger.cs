using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilityTrigger : MonoBehaviour
{
    public bool unlockLightPulse = false;
    public bool unlockFearSense = false;
    public bool enableShooting = false; // If you want to enable the ABShooting script later

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            ABShooting abs = other.GetComponent<ABShooting>();

            if (pc != null)
            {
                if (unlockLightPulse) pc.UnlockLightPulseAbility();
                if (unlockFearSense) pc.UnlockFearSenseAbility();
            }

            if (abs != null && enableShooting)
            {
                abs.EnableShooting();
            }
            
            // Destroy the trigger after the player passes through it once
            Destroy(gameObject); 
        }
    }
}

