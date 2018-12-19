using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour {

    SpellProjectile instance;
    Vector2 movement;
    bool yIsPressed;
    bool yIsReleased;
    bool xIsPressed;
    bool xIsReleaed;

	// Use this for initialization
	void Start ()
    {
        instance = GameObject.Find("GameManager").GetComponent<SpellProjectile>();
	}
	
    void FixedUpdate()
    {
        yIsPressed = Input.GetButtonDown("AimVertical");
        yIsReleased = Input.GetButtonUp("AimVertical");
        xIsPressed = Input.GetButtonDown("AimHorizontal");
        xIsReleaed = Input.GetButtonUp("AimHorizontal");
        Aim();
    }

    private void Update()
    {
        transform.position = GameObject.Find("Player").transform.position;
    }

    private void LateUpdate()
    {
        instance.Fire();
    }

    void Aim() //not perfect. still sticks a bit, especially when trying to aim the opposite direction. Not sure how to improve, as Aim needs to be in FixedUpdate.
    {
        movement = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));

        if (yIsPressed || xIsReleaed)
        {
            if (movement.y == 1)
            {
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(90);
            }
            else if (movement.y == -1)
            {
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(270);
            }
        }

        if (xIsPressed || yIsReleased)
        {
            if (movement.x == -1)
            {
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(180);
            }
            else if (movement.x == 1)
            {
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(0);
            }
        }
    }
}
