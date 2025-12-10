using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NullZone : MonoBehaviour
{
    [Header("Null Zone Settings")]
    public float damageInterval = 1.0f; // How often the player loses health
    public int damagePerTick = 1;       // Damage applied each tick

    private float timer = 0f;
    private PlayerStats player;
    private NullZoneAudioMuffle audioFx;

    public float gravityInZone = 0.4f;
    public float speedMultiplier = 0.7f;

    private PlayerController controller;
    private Rigidbody2D rb;
    private float originalMoveSpeed;
    private float originalGravity;

    void Start()
    {
        audioFx = FindObjectOfType<NullZoneAudioMuffle>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            player = col.GetComponent<PlayerStats>();

        if (col.CompareTag("Player"))
            audioFx?.EnterNullZone();

        if (col.CompareTag("Player"))
        {
            controller = col.GetComponent<PlayerController>();
            rb = col.GetComponent<Rigidbody2D>();

            originalMoveSpeed = controller.moveSpeed;
            originalGravity = rb.gravityScale;

            rb.gravityScale = gravityInZone;
            controller.moveSpeed *= speedMultiplier;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player = null;
            timer = 0f;
        }


        if (col.CompareTag("Player"))
        {
            player = null;

            audioFx?.ExitNullZone();      
            timer = 0f;
        }


      

        if (col.CompareTag("Player"))
        {
            rb.gravityScale = originalGravity;
            controller.moveSpeed = originalMoveSpeed;
        }
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= damageInterval)
        {
            // Null Zone ignores invincibility
            player.TakeNullZoneDamage(damagePerTick);
            timer = 0f;
        }
    }


   

}
