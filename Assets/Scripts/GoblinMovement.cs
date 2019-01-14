using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    Transform target;
    Rigidbody2D rb;
    float speed;
    LayerMask toHit;

    // Start is called before the first frame update
    void Start()
    {
        target = Player.instance.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        speed = gameObject.GetComponent<Goblin>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        Facing();
    }

    void Facing()
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


    void Movement()
    {
        //returns true if there is a collider between gameObject and target
        if (Physics2D.Linecast(transform.position, target.position, toHit))
        {
            Debug.Log($"{Physics2D.Linecast(transform.position, target.position, toHit).collider} is between {gameObject} and {target}");
            if (Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
            {
                if (target.position.x > transform.position.x)
                {
                    //move right
                }
                else
                {
                    //move left
                }
            }
            else
            {
                if (target.position.y > transform.position.y)
                {
                    //move up
                }
                else
                {
                    //move down
                }
            }
        }


        //stop moving when going to shoot bow

        //maybe move away from target while attack is on cooldown?
    }
}
