using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed; 
    public float jumpHeight; 
    public KeyCode Spacebar; 
    public KeyCode L;
    public KeyCode R;
    public KeyCode GrabKey; // Key for grabbing/releasing
    public float grabRadius; // Radius to detect nearby objects
    public Transform groundCheck; 
    public float groundCheckRadius; 
    public LayerMask whatIsGround; 
    
    private bool grounded; 
    private bool canDoubleJump;
    private bool isGrabbing = false; 
    private PushableObject grabbedObject = null; 

    // Cached References
    private Animator anim;
    private Rigidbody2D rb; 
    private SpriteRenderer spriteRenderer; 

    // --- Level 1 Abilities Additions ---
    [Header("Level 1 Abilities")]
    public KeyCode FearSenseKey = KeyCode.E; // Key to activate Fear Sense
    public KeyCode LightPulseKey = KeyCode.Q; // Key to activate Light Pulse
    public bool hasFearSense = false; // Unlocked in Scene 3
    public bool hasLightPulse = false; // Unlocked in Scene 2
    public float abilityDuration = 3f; // How long Fear Sense lasts
    public float abilityCooldown = 5f;
    private bool onCooldown = false;
    public float pulseRadius = 5f; // Radius for light pulse interaction - this is no longer used for the projectile approach

    [Header("Visual Effects")] 
    // public GameObject lightPulseEffectPrefab; // REMOVED: Replaced with the projectile prefab
    public GameObject lightPulseProjectilePrefab; // NEW: Drag your projectile prefab here in the Inspector


	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null) Debug.LogError("Rigidbody2D missing!");
    }
	
	void Update () {
        // Animation updates in Update()
       

        // Input gathering in Update()
        HandleJumping();
       
        // New Grab/Release Input Logic
        if (Input.GetKeyDown(GrabKey))
        {
            if (grabbedObject == null) TryGrabObject();
            else ReleaseObject();
        }

        // --- Handle New Abilities Input ---
        if (hasFearSense && Input.GetKeyDown(FearSenseKey) && !onCooldown)
        {
            StartCoroutine(ActivateFearSense());
        }

        // Changed: Now uses the new projectile logic
        if (hasLightPulse && Input.GetKeyDown(LightPulseKey))
        {
            ActivateLightPulse();
        }
    }

    void FixedUpdate()
    { 
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); 

        if (grounded) canDoubleJump = true;
        
        HandleMovement();
    }

    // Handles movement using forces when needed
    void HandleMovement()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(L)) horizontalInput = -1f;
        if (Input.GetKey(R)) horizontalInput = 1f;

        if (isGrabbing)
        {
            rb.AddForce(new Vector2(horizontalInput * moveSpeed * 50 * Time.deltaTime, 0)); 
            if (Mathf.Abs(rb.velocity.x) > moveSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        if (horizontalInput < 0) spriteRenderer.flipX = true;
        else if (horizontalInput > 0) spriteRenderer.flipX = false;
    }

    void HandleJumping()
    {
        if(Input.GetKeyDown(Spacebar) && !isGrabbing) 
        {
            if (grounded) Jump();   
            else if (canDoubleJump) { Jump(); canDoubleJump = false; }
        }
    }

    void TryGrabObject()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(groundCheck.position, grabRadius);
        foreach (Collider2D hit in hitColliders)
        {
            grabbedObject = hit.GetComponent<PushableObject>();
            if (grabbedObject != null)
            {
                grabbedObject.ConnectToPlayer(rb); 
                isGrabbing = true; return; 
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.DisconnectFromPlayer();
            grabbedObject = null;
            isGrabbing = false;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);   
    }

    // --- New Ability Implementations ---

    IEnumerator ActivateFearSense()
    {
        onCooldown = true;
        GhostEnemy[] guards = FindObjectsOfType<GhostEnemy>(); 
        foreach (var guard in guards)
        {
            guard.SetHighlighted(true);
        }
        // Play Echo's audio line here: "Warning Warning a lot of enemies detected..."

        yield return new WaitForSeconds(abilityDuration);

        foreach (var guard in guards)
        {
            guard.SetHighlighted(false);
        }

        yield return new WaitForSeconds(abilityCooldown - abilityDuration);
        onCooldown = false;
    }
    
    // MODIFIED: Uses Instantiate and Launch now
    void ActivateLightPulse()
    {
        if (lightPulseProjectilePrefab != null)
        {
            // Determine direction based on player flipX state
            Vector2 launchDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

            // Instantiate the projectile slightly in front of the player
            GameObject pulseGO = Instantiate(lightPulseProjectilePrefab, transform.position + (Vector3)launchDirection * 0.5f, Quaternion.identity);
            
            // Get the projectile script and launch it
            LightPulseProjectile pulseScript = pulseGO.GetComponent<LightPulseProjectile>();
            if (pulseScript != null)
            {
                pulseScript.Launch(launchDirection);
            }
        }
        else
        {
            Debug.LogWarning("lightPulseProjectilePrefab is missing! Drag a projectile prefab into the PlayerController inspector.");
        }
        
        // OLD CODE REMOVED: OverlapCircleAll is no longer used here.
    }

    public void UnlockFearSenseAbility()
    {
        hasFearSense = true;
        Debug.Log("Fear Sense Unlocked!");
    }

    public void UnlockLightPulseAbility()
    {
        hasLightPulse = true;
        Debug.Log("Light Pulse Unlocked!");
    }

    
}







  