using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMG : MonoBehaviour
{
    public float damageIncrease;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.playerDamage += damageIncrease;
            Destroy(gameObject);
        }
    }
}
