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
    float projectileSpeedScale = 0.025f;


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

    //see if using quaternions would make this work better, and if so how.
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
        Vector2 attackAngle = new Vector2(1.5f, 1.5f);

        if (Physics2D.Linecast(transform.position, target.position, toHit) == false && Time.time > nextShot)
        {
            nextShot = Time.time + (1/rateOfFire);
            isAttacking = true;
            //find angle between goblin and player
            Vector2 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 180f; //not sure if this - 180f is right/necessary.
            //I think this can be used to rotate the projectile towards the player.
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            // use Vector2.Perpendicular(targetDir) as direction of ray, not end point of line, unless I can figure out how to adjust it.
            Ray2D ray = new Ray2D(target.position, Vector2.Perpendicular(targetDir));
            Vector2 maxHit = ray.GetPoint(1f);
            Vector2 minHit = ray.GetPoint(-1f);
            Vector2 attackArc = maxHit - minHit;
            Vector2 attackPoint = minHit + Random.value * attackArc;

            //figure out how to wait some time, since attack is slow.
            //calculate frame/time delay self in code.

            GameObject clone = GameObject.Find("ArrowPool").GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = spawnPosition;
            clone.transform.rotation = spawnRotation;
            clone.SetActive(true);
            clone.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectileSpeedScale * projectileSpeed, 0));


            isAttacking = false;
        }
    }

    private void OnDisable()
    {
        room.enemiesInRoom.Remove(gameObject);
    }
}
