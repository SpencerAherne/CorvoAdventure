using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public new Transform transform;
    Rigidbody2D rb;
    public Vector2 movement;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("MoveHorizontal"), Input.GetAxisRaw("MoveVertical"));
    }

    public void Move()
    {
        rb.MovePosition((Vector2)transform.position + (movement * Player.instance.playerSpeed * Time.deltaTime));

        if (Input.GetButtonDown("MoveVertical") || Input.GetButtonUp("MoveHorizontal"))
        {
            if (movement.y == 1)
            {
                rb.MoveRotation(90);
            }
            else if (movement.y == -1)
            {
                rb.MoveRotation(270);
            }
        }

        if (Input.GetButtonDown("MoveHorizontal") || Input.GetButtonUp("MoveVertical"))
        {
            if (movement.x == -1)
            {
                rb.MoveRotation(180);
            }
            else if (movement.x == 1)
            {
                rb.MoveRotation(0);
            }
        }
    }
}
