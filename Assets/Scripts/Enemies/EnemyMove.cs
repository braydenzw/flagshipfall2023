using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public float speed;

    // closest enemy should move to player
    public float range;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // Check if the enemy is within the stopping distance
        if (distanceToPlayer > range)
        {
            // Move the enemy towards the player
            rb.velocity = direction * speed;
        }
        else
        {
            // Stop the enemy's movement
            rb.velocity = Vector2.zero;
        }
    }
}
