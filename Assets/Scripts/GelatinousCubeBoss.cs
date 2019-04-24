using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelatinousCubeBoss : MonoBehaviour
{
    #region Singleton
    public static GelatinousCubeBoss instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public float maxHealth;
    float curHealth;
    public float damage;
    public float speed;
    Rigidbody2D rb;
    bool isMoving = false;
    public float stillTime;
    Vector2 endPos;
    public TrailRenderer trail;
    public float debuffDuration = 3f;
    public float slowRate = .5f;
    Room room;
    public GameObject trophy;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        trail = GetComponent<TrailRenderer>();
        room = GetComponentInParent<Room>();
    }

    private void Update()
    {

    }

    //If boss runs into wall, then picks a second location in which they never leave contact with the wall, it doesn't pick another point to move to.
    void FixedUpdate()
    {
        if (endPos == null)
        {
            endPos = Movement();
        }
        else if (endPos == (Vector2)transform.position)
        {
            StartCoroutine(StopMoving());
            if (isMoving == false)
            {
                endPos = Movement();
            }
        }
        rb.MovePosition(Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime));
        Debug.DrawLine(transform.position, endPos);
    }

    private Vector2 Movement()//ask if this works well.
    {
        Debug.Log("Movement has been called");
        //Would be better if I could use bounds.min/bounds.max, but would require remaking how rooms are made.
        float xMin = -8.2f;
        float xMax = 8.2f;
        float yMin = -3.9f;
        float yMax = 3.9f;
        //make boss move randomly around the room, hurting player on contact, as well as leaving a trail that slows and/or damages the player and goes away after some time.
        //Maybe make a bunch of points for the boss to move to and from randomly, unless there is a method/class/object that does that for me.
        float xPos = Random.Range(xMin, xMax);
        float yPos = Random.Range(yMin, yMax);
        Vector2 endPos = new Vector2(xPos, yPos);
        return endPos;
    }

    IEnumerator StopMoving()//Doesn't actually stop the boss. Ask if this feels right or not.
    {
        yield return new WaitForSecondsRealtime(stillTime);
        isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.DamagePlayer(damage);
        }

        if (collision.gameObject.layer == 8)
        {
            return;
        }
        else
        {
            Debug.Log("onCollisionEnter2D was called");
            StartCoroutine(StopMoving());
            endPos = Movement();
            rb.MovePosition(Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            return;
        }
        else if (collision.gameObject.layer == 8)
        {
            return;
        }
        else
        {
            Debug.Log("onCollisionEnter2D was called");
            StartCoroutine(StopMoving());
            endPos = Movement();
            rb.MovePosition(Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime));
        }
    }

    public void DamageBoss(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            GameObject clone = Instantiate(trophy);
            clone.transform.position = gameObject.transform.parent.position;
            clone.transform.parent = gameObject.transform.parent;
            Destroy(gameObject);
            room.enemiesInRoom.Remove(gameObject);
            //Boss loot would drop here if I decide to add it
        }
    }
}
