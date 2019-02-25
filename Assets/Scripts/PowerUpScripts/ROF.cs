﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROF : MonoBehaviour
{
    public float rofIncrease;
    public float pickUpTime = 1.5f;

    private void Start()
    {
        SpawnDelay();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.playerFireRate += rofIncrease;
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnDelay()
    {
        BoxCollider2D bc = gameObject.GetComponent<BoxCollider2D>();
        bc.enabled = false;
        yield return new WaitForSecondsRealtime(pickUpTime);
        bc.enabled = true;
    }
}
