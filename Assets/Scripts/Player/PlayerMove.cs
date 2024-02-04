using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalIn = 0;
        float verticalIn = 0;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalIn++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalIn--;
        }
        if (Input.GetKey(KeyCode.W))
        {
            verticalIn++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalIn--;
        }

        Vector2 move;
        if (horizontalIn != 0 && verticalIn != 0)
        {
            move = new Vector2(horizontalIn * speed, verticalIn * speed * 0.5f);
        }
        else
        {
            // fixing to isometric (1, 0.5)
            move = new Vector2(horizontalIn * speed, verticalIn * speed);
        }
        rb.velocity = move;
    }
}
