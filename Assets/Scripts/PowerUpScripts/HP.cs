using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int healthIncrease = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.playerTotalHealth += healthIncrease;
            Player.instance.playerCurHealth += healthIncrease;
            Destroy(gameObject);
        }
    }
}
