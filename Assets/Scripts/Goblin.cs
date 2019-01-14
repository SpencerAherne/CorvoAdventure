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

    // Start is called before the first frame update
    void Start()
    {
        room = gameObject.GetComponentInParent<Room>();
        curHealth = maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        room.enemiesInRoom.Remove(gameObject);
    }
}
