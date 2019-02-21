using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelatinousCubeBoss : MonoBehaviour
{
    public float maxHealth;
    float curHealth;
    public float damage;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Movement()
    {
        //make boss move randomly around the room, hurting player on contact, as well as leaving a trail that slows and/or damages the player and goes away after some time.
        //Maybe make a bunch of points for the boss to move to and from randomly, unless there is a method/class/object that does that for me.
    }
}
