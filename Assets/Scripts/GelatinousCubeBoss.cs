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
    new Collider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving == false)
        {
            Movement();
            isMoving = true;
        }
    }

    private void Movement()//ask if this works well.
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
        //if point endpos is withing the boss, it breaks to restart the method, rather than moving to said position.
        rb.MovePosition(Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime));
        if ((Vector2)transform.position == endPos)
        {
            StopMoving();
        }
    }

    IEnumerator Trail()
    {
        //leave behind trail of slime that slows or damages player
        yield break;
    }

    IEnumerator StopMoving()
    {
        yield return new WaitForSecondsRealtime(stillTime);
        isMoving = false;
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
