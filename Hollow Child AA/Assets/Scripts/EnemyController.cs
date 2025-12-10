using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected float maxSpeed = 2f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected int health = 3; 
    
    public SpriteRenderer sr; 

    protected void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) Debug.LogError("EnemyController is missing a SpriteRenderer component!");
    }

    public void Flip()
    {
        sr.flipX = !sr.flipX;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null) playerStats.TakeDamage(damage);
            Flip();
        }
        else if (other.CompareTag("Wall"))
        {
            Flip();
        }
    }

    public void EnemyTakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0; 
            Debug.Log("Enemy Defeated");
            // FindObjectOfType<SpawnEnemy>().RespawnEnemy() is removed from here 
            // because the spawner is used for specific level events now, not general enemy death.
            Destroy(gameObject);
        }
    }
}




