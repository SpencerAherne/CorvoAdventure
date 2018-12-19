using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float MaxHealth;
    float CurrentHealth;
    public float Damage;
    public float Speed;
    public float FireRate;
    public float ProjectileSpeed;

    // Use this for initialization
    void Start ()
    {
        CurrentHealth = MaxHealth;
	}

	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.DamagePlayer(Damage);
        }
    }

    public void DamageSkeleton(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
