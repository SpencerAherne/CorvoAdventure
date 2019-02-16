﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;

    public static Player instance;

    public int keyCount;
    public int gemCount;
    public int arcanePulseCount;
    public float playerMaxHealth;
    public float playerCurHealth;
    public float playerSpeed = 10f;
    public float playerDamage;
    public float playerFireRate;
    public float playerProjectileSpeed;
    public Room currentRoom;

    public GameObject arcanePulse;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        playerCurHealth = playerMaxHealth;
        playerMovement = GetComponent<PlayerMovement>();
	}

    private void Update()
    {
        playerMovement.Move();
        UseArcanePulse();
    }

    void FixedUpdate ()
    {
        if (playerCurHealth > playerMaxHealth)
        {
            playerCurHealth = playerMaxHealth;
        }
	}

    public void DamagePlayer(float damage)
    {
        playerCurHealth -= damage;
        if (playerCurHealth <= 0)
        {
            //kill player/end game
        }
        //give invul frames
    }

    void UseArcanePulse()
    {
        if (Input.GetKeyDown(KeyCode.Q) && arcanePulseCount > 0)
        {
            Debug.Log("UseArcanePulse was called");
            //arcane pulse spawns in center of room if position and rotation aren't given, but doesn't spawn at all when position and rotation are given in this manner.
            Instantiate(arcanePulse, transform.position, transform.rotation); 
            arcanePulseCount -= 1;
        }
    }
}
