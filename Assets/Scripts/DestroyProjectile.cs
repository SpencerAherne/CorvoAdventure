﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{


    private void Start()
    {

    }

    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Skeleton")
            {
                collision.gameObject.GetComponent<Skeleton>().DamageSkeleton(Player.instance.playerDamage);
                gameObject.SetActive(false);
            }
            //this is if instead of else as I plan on making at least one more enemy. Not sure if I should make else until I make the other enemies or if this is fine.
            if (collision.gameObject.name == "Goblin")
            {
                collision.gameObject.GetComponent<Goblin>().DamageGoblin(Player.instance.playerDamage);
            }
            
        }
        else if (collision.gameObject.tag == "Destroyable")
        {
            collision.gameObject.GetComponent<Destroyable>().DamageObject(Player.instance.playerDamage);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

}
