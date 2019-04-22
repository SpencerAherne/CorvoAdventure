using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour {

    SpellProjectile instance;

	// Use this for initialization
	void Start ()
    {
        instance = GameObject.Find("GamePlayManager").GetComponent<SpellProjectile>();
	}
	
    void FixedUpdate()
    {
        Aim();
    }

    private void Update()
    {
        transform.position = Player.instance.transform.position;
    }

    private void LateUpdate()
    {
        instance.Fire();
    }

    void Aim() //not perfect. Aiming has a key heirarchy I don't like, but don't know how to avoid.
    {
        if (Input.GetButton("AimUp"))
        {
            gameObject.GetComponent<Rigidbody2D>().MoveRotation(90);
        }
        else if (Input.GetButton("AimDown"))
        {
            gameObject.GetComponent<Rigidbody2D>().MoveRotation(270);
        }
        else if (Input.GetButton("AimLeft"))
        {
            gameObject.GetComponent<Rigidbody2D>().MoveRotation(180);
        }
        else if (Input.GetButton("AimRight"))
        {
            gameObject.GetComponent<Rigidbody2D>().MoveRotation(0);
        }
    }
}
