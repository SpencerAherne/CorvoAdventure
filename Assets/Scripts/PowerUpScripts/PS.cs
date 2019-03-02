using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS : MonoBehaviour
{
    public float projectileSpeedIncrease;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.playerProjectileSpeed += projectileSpeedIncrease;
            Destroy(gameObject);
        }
    }
}
