using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelatinousCubeBoss : MonoBehaviour
{
    public float maxHealth;
    float curHealth;
    public float damage;
    public float speed;
    Rigidbody2D rb;
    bool isMoving = false;
    public float stillTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            Movement();
        }
    }

    IEnumerator Movement()//ask if this works well.
    {
        isMoving = true;
        float xMin = -8.2f;
        float xMax = 8.2f;
        float yMin = -3.9f;
        float yMax = 3.9f;
        //make boss move randomly around the room, hurting player on contact, as well as leaving a trail that slows and/or damages the player and goes away after some time.
        //Maybe make a bunch of points for the boss to move to and from randomly, unless there is a method/class/object that does that for me.
        float xPos = Random.Range(xMin, xMax);
        float yPos = Random.Range(yMin, yMax);
        Vector2 endPos = new Vector2(xPos, yPos);
        rb.MovePosition(Vector2.MoveTowards(transform.position, endPos, speed));
        if ((Vector2)transform.position == endPos)
        {
            yield return new WaitForSecondsRealtime(stillTime);
            isMoving = false;
        }
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            yield break;
        }
        else
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, transform.position, speed));
            yield return new WaitForSecondsRealtime(stillTime);
            isMoving = false;
        }
    }
}
