using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Transform target;
    Rigidbody2D rb;
    float speed;

    // Use this for initialization
    void Start ()
    {
        target = Player.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        speed = GetComponent<Skeleton>().speed;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        EnemyMovement();
        rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
    }

    //Right now skeltons are shaped weird when looking north/south. Need to fix.
    void EnemyMovement()
    {
        if (Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
        {
            if (target.position.x > transform.position.x)
            {
                rb.MoveRotation(0);
            }
            else
            {
                rb.MoveRotation(180);
            }
        }
        else
        {
            if (target.position.y > transform.position.y)
            {
                rb.MoveRotation(90);
            }
            else
            {
                rb.MoveRotation(270);
            }
        }
    }
}
