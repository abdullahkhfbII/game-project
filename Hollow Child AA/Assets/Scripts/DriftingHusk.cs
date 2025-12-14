using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DriftingHusk : MonoBehaviour
{

    public float speed = 0.6f;
    public float observeDistance = 10f;

    Transform player;
    SpriteRenderer playerSprite;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.transform;
        playerSprite = p.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!PlayerLookingAtMe())
        {
            MoveTowardPlayer();
        }
    }

    void MoveTowardPlayer()
    {
        if (!player) return;

        float direction =
            player.position.x > transform.position.x ? 1f : -1f;

        transform.position +=
            Vector3.right * direction * speed * Time.deltaTime;
    }

    bool PlayerLookingAtMe()
    {
        if (!player) return false;

        // Distance check
        if (Vector2.Distance(transform.position, player.position) > observeDistance)
            return false;

        // Direction from player to husk
        Vector2 toHusk = (transform.position - player.position).normalized;

        // Player facing direction (flipX-based)
        Vector2 playerFacing =
            playerSprite.flipX ? Vector2.left : Vector2.right;

        float dot = Vector2.Dot(playerFacing, toHusk);

        return dot > 0.65f;
    }
}
