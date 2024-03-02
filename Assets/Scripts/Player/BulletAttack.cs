using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public GameObject bullet;
    public float speed = 5f;

    public Rigidbody2D player;

    private Vector3 lastVel;
    private Vector3 lastPos;
    public float cooldown = 1f;
    private float timer;

    private void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
        lastVel = new Vector3(1f, 0, 0);
        timer = 0f;

        lastPos = this.transform.position;
    }

    void Update()
    {
        // checking keydown to fire
        if ((timer >= cooldown) && Input.GetKey(KeyCode.Space))
        {
            // create bullet object and set its velocity direction
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<BulletBehavior>().v = lastVel * speed;

            // start cooldown timer
            timer = 0f;
        }

        // update timer if cooldown active
        if (timer <= cooldown) {
            timer += Time.deltaTime;
        }


        parseMoveDirection();
    }

    private void parseMoveDirection()
    {
        float x = 0;
        float y = 0;
        if (Input.GetKey(KeyCode.D))
        {
            x++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x--;
        }
        if (Input.GetKey(KeyCode.W))
        {
            y++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y--;
        }

        if (x != 0 || y != 0)
        {
            lastVel = new Vector3(x, y, 0);
        }

        if(x != 0 && y != 0)
        {
            // fixing to isometric
            lastVel = new Vector3(x, y * 0.5f, 0);
        }
    }
}
