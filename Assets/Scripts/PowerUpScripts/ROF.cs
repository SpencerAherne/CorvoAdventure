using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROF : MonoBehaviour
{
    public float rofIncrease;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.playerFireRate += rofIncrease;
            Destroy(gameObject);
        }
    }
}
