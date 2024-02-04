using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletBehavior : MonoBehaviour
{
    public CircleCollider2D col;
    public Rigidbody2D r;
    public Vector3 v;

    public float lifeTime = 3f;

    private void Start()
    {
        // initialize components, set velocity speed
        col = this.GetComponent<CircleCollider2D>();

        // destroy object after x seconds (for playtesting)
        Destroy(this.gameObject, lifeTime);
    }

    private void Update()
    {
        // maintain constant velocity
        r.velocity = v;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // do something if collision w/ enemy
        if(collision.tag == "Enemy")
        {
            // do something
        }
    }
}
