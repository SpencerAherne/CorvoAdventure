using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public Sprite openChest;
    public GameObject HPPowerUp;
    public GameObject DMGPowerUp;
    public GameObject RateOfFiePowerUp;
    public GameObject ProjectileSpeedPowerUp;
    public GameObject PlayerSpeedPowerUp;

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Player.instance.keyCount > 0 && spriteRenderer.sprite != openChest)
            {
                spriteRenderer.sprite = openChest;
                Player.instance.keyCount--;
                SpawnPowerUp(collision);
            }
        }
    }

    //TODO: Make loot spawn either on oposite side of chest, or make it so it can't be picked up for a second when it spawns.
    void SpawnPowerUp(Collision2D player)
    {
        int roll = Random.Range(0, 5);
        if (roll == 0)
        {
            Instantiate(HPPowerUp, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 1)
        {
            Instantiate(DMGPowerUp, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 2)
        {
            Instantiate(PlayerSpeedPowerUp, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 3)
        {
            Instantiate(RateOfFiePowerUp, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 4)
        {
            Instantiate(ProjectileSpeedPowerUp, -player.transform.position, gameObject.transform.rotation);
        }
    }
}
