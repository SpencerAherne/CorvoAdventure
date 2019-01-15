using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{

    public float maxHealth;
    float curHealth;
    public float speed;
    public float damage;
    public float rateOfFire;
    public float projectileSpeed;
    Room room;

    Transform target;
    Rigidbody2D rb;
    public LayerMask toHit;
    bool isAttacking;
    float nextShot;
    public GameObject spawn;


    // Start is called before the first frame update
    void Start()
    {
        target = Player.instance.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        room = gameObject.GetComponentInParent<Room>();
        curHealth = maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking == false)
        {
            Facing();
            Movement();
        }

    }

    void Facing()
    {
        if (Mathf.Abs(target.position.x - transform.position.x) > Mathf.Abs(target.position.y - transform.position.y))
        {
            if (target.position.x > transform.position.x)
            {
                rb.MoveRotation(180);
            }
            else
            {
                rb.MoveRotation(0);
            }
        }
        else
        {
            if (target.position.y > transform.position.y)
            {
                rb.MoveRotation(270);
            }
            else
            {
                rb.MoveRotation(90);
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
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(100, 0), speed * Time.deltaTime));
                }
                else
                {
                    //move left
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(-100, 0), speed * Time.deltaTime));
                }
            }
            else
            {
                if (target.position.y > transform.position.y)
                {
                    //move up
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(0, 100), speed * Time.deltaTime));
                }
                else
                {
                    //move down
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(0, -100), speed * Time.deltaTime));
                }
            }
        }

        //maybe move away from target while attack is on cooldown?
    }

    void Attack()
    {
        Vector2 spawnPosition = spawn.transform.position;
        Quaternion spawnRotation = spawn.transform.rotation;

        if (Physics2D.Linecast(transform.position, target.position, toHit) == false && Time.time > nextShot)
        {
            nextShot = Time.time + (1/rateOfFire);
            isAttacking = true;
            GameObject clone = GameObject.Find("ArrowPool").GetComponent<ObjectPooler>().GetPooledObject();
            //figure out how to wait some time, since attack is slow.
            

            isAttacking = false;
        }
    }

    private void OnDisable()
    {
        room.enemiesInRoom.Remove(gameObject);
    }
}
