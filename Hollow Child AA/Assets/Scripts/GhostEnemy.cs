using System.Collections;
using UnityEngine;

public class GhostEnemy : EnemyController
{
    [SerializeField] private float chaseRange = 5f;      // How far the enemy will see the player
    [SerializeField] private float attackRange = 1.5f;   // Distance to trigger attack
    [SerializeField] private float moveSpeed = 2f;       // Movement speed

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isAttacking = false;
    private bool isDead = false;

    private Color defaultColor;
    public bool isHighlighted = false;
    public Color highlightColor = Color.green;

    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null) Debug.LogError("GhostEnemy is missing a Rigidbody2D component!");
        if (animator == null) Debug.LogError("GhostEnemy is missing an Animator component!");

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) player = playerObject.transform;

        if (sr != null) defaultColor = sr.color;
    }

    void Update()
    {
        if (isDead) return;

        // Face player
        if (player != null && sr != null)
            sr.flipX = (player.position.x < transform.position.x);

        // Update walking animation
        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Check for attack
        if (player != null && !isAttacking)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead || isAttacking) 
        {
            rb.velocity = Vector2.zero; // Stop moving if dead or attacking
            return;
        }

        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= chaseRange && distance > attackRange)
        {
            // Move toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            // Stop moving if out of chase range or too close
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; // Stop moving during attack

        if (animator != null)
            animator.SetBool("IsAttacking", true);

        // Wait for attack animation to complete (adjust to match animation length)
        yield return new WaitForSeconds(0.5f);

        // Deal damage if player still in range
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            if (stats != null) stats.TakeDamage(damage);
        }

        isAttacking = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
            Die();
        else
            animator.SetTrigger("Hurt");
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        if (animator != null)
            animator.SetBool("IsDead", true);

        Destroy(gameObject, 1.5f);
    }

    public void SetHighlighted(bool highlight)
    {
        isHighlighted = highlight;

        if (sr != null)
        {
            sr.color = highlight ? highlightColor : defaultColor;
            sr.enabled = true; // Ensure sprite always visible
        }
    }
}
