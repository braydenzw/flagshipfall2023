using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float stoppingDrag = 5f; // Adjust this value to control how quickly the character stops

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        // Get input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        // Move the player using Rigidbody2D
        rb.velocity = movement * speed;

        // Apply stopping drag when no input is received
        if (Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f))
        {
            rb.velocity *= Mathf.Pow(1f - stoppingDrag * Time.deltaTime, 50f); // Use a power to simulate a smoother damping effect
        }
    }
}
