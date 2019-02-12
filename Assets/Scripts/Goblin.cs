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
    public float projectileSpeedScale = 1f;

    public float chargeTime;
    public float attackTime;
    public float constant;
    public float offset;

    public Transform player;

    Quaternion rotation;

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
        rotation = Quaternion.LookRotation(transform.position - target.position);


        if (isAttacking == false)
        {
            Facing();
            Movement();
        }
    }

    private void FixedUpdate()
    {
        Attack();
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

        if (Physics2D.Linecast(transform.position, target.position, toHit) == false && Time.time > nextShot)
        {
            nextShot = Time.time + (1/rateOfFire);
            isAttacking = true;
            //find angle between goblin and player
            Vector2 targetDir = target.position - transform.position;

            // use Vector2.Perpendicular(targetDir) as direction of ray, not end point of line, unless I can figure out how to adjust it.
            Ray2D ray = new Ray2D(target.position, Vector2.Perpendicular(targetDir));
            Vector2 maxHit = ray.GetPoint(1f);
            Vector2 minHit = ray.GetPoint(-1f);
            Vector2 attackArc = maxHit - minHit;
            Debug.Log($"attackArc = {attackArc}");
            Vector2 attackPoint = minHit + Random.value * attackArc;
            //order of x,y matter for Atan2, not sure if that's my problem
            float attackAngle = Mathf.Atan2(attackPoint.y, attackPoint.x) * Mathf.Rad2Deg; //add or subtract float to correct for rotation.
            Debug.Log($"attackAngle is {attackAngle}");

            //figure out how to wait some time, since attack is slow.
            //calculate frame/time delay self in code.
            while (chargeTime <= attackTime)
            {
                chargeTime++;
            }

            GameObject clone = GameObject.Find("StonePool").GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = transform.position;
            Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            clone.transform.rotation = Quaternion.LookRotation(attackPoint);
            clone.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle + offset);
            clone.SetActive(true);
            clone.GetComponent<Rigidbody2D>().velocity = (attackPoint - (Vector2)clone.transform.position).normalized * constant;

            Debug.DrawLine(clone.transform.position, attackPoint, Color.magenta, 5);

            isAttacking = false;
        }
    }

    private void OnDisable()
    {
        room.enemiesInRoom.Remove(gameObject);
    }
}
