using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{

    Transform target;
    Rigidbody2D rb;
    float speed;

    // Use this for initialization
    void Start ()
    {
        target = Player.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        speed = GetComponent<Skeleton>().Speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        EnemyMovement();
        rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
    }

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
