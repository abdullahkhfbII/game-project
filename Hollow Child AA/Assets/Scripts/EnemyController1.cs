using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Maximum movement speed of the enemy.")]
    [SerializeField] protected float maxSpeed = 2f; // Changed to protected
    [Tooltip("Damage dealt to the player on collision.")]
    [SerializeField] protected int damage = 1;     // Changed to protected
    [Tooltip("Total health points of the enemy.")]
    [SerializeField] protected int health = 3;     // Changed to protected
    
    public SpriteRenderer sr; 

    protected void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) Debug.LogError("EnemyController is missing a SpriteRenderer component!", this); 
        // Note: Boss will override this Start() or call base.Start()
    }

    // ... (Flip() and OnTriggerEnter2D() remain the same, though the boss might not use them) ...
    public void Flip()
    {
        if (sr != null) sr.flipX = !sr.flipX;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null) playerStats.TakeDamage(damage);
        }
        else if (other.CompareTag("Wall"))
        {
            Flip();
        }
    }


    // CRITICAL: Must be virtual to be overridden in BossEyeController
    public virtual void EnemyTakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0; 
            Debug.Log(gameObject.name + " Defeated");
            DefeatAction(); // Use a virtual method for specific defeat actions
        }
        else
        {
            StartCoroutine(DamageFlashRoutine());
        }
    }

    // A new virtual method that can be overridden by the boss script
    protected virtual void DefeatAction()
    {
        // Default behavior for normal enemies
        Destroy(gameObject);
    }

    protected IEnumerator DamageFlashRoutine()
    {
        if (sr != null)
        {
            Color originalColor = sr.color;
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor;
        }
    }
}






