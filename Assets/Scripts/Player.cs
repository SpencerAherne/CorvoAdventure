using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;

    public static Player instance;

    public int keyCount;
    public int gemCount;
    public float playerMaxHealth;
    public float playerCurHealth;
    public float playerSpeed = 10f;
    public float playerDamage;
    public float playerFireRate;
    public float playerProjectileSpeed;
    private Room currentRoom;

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
}
