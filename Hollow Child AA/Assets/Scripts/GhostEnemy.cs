using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : EnemyController
{
    [SerializeField] private float flickerDuration = 0.2f; 
    private Transform player; 
    private Rigidbody2D rb; 
    private Color defaultColor; 
    public bool isHighlighted = false; // Track if currently highlighted by player ability
    public Color highlightColor = Color.green; // Color when highlighted

    new void Start()
    {
        base.Start(); 
        rb = GetComponent<Rigidbody2D>(); 
        if (rb == null) Debug.LogError("GhostEnemy is missing a Rigidbody2D component!");

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) player = playerObject.transform;
        else Debug.LogError("Player object not found! Make sure your Player has the 'Player' tag.");
        
        if (sr != null) defaultColor = sr.color;

        StartCoroutine(SpriteFlickerRoutine());
    }

    void Update()
    {
        if (player != null && sr != null) sr.flipX = (player.position.x < transform.position.x);
    }

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(directionToPlayer.x * maxSpeed, 0); 
        }
    }
    
    // --- NEW METHOD for Fear Sense Ability Integration ---
    public void SetHighlighted(bool highlight)
    {
        isHighlighted = highlight;
        
        if (sr != null)
        {
            if (highlight)
            {
                sr.color = highlightColor;
                StopCoroutine(SpriteFlickerRoutine()); 
                sr.enabled = true; // Ensure visibility while highlighted
            }
            else
            {
                sr.color = defaultColor;
                StartCoroutine(SpriteFlickerRoutine());
            }
        }
    }

    IEnumerator SpriteFlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flickerDuration);
            if (sr != null && !isHighlighted)
            {
                sr.enabled = !sr.enabled;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null) playerStats.TakeDamage(damage); 
        }
    }
}





