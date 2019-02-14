using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    public float damage;
    public float speed;
    Room room;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        room = gameObject.GetComponentInParent<Room>();

        currentHealth = maxHealth;
	}

	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.DamagePlayer(damage);
        }
    }

    public void DamageSkeleton(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        room.enemiesInRoom.Remove(gameObject);
    }
}
