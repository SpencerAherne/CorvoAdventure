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
    public float pickUpTime = 1.5f;
    public float radius = 1.5f;
    public LayerMask toHit;

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

    void SpawnPowerUp(Collision2D player)
    {
        RaycastHit2D spawnClear;
        Vector2 spawnPoint;

        spawnPoint = (Vector2)gameObject.transform.position + Random.insideUnitCircle * radius;
        spawnClear = Physics2D.Linecast(gameObject.transform.position, spawnPoint, toHit);

        while (spawnClear != false)
        {
            spawnPoint = (Vector2)gameObject.transform.position + Random.insideUnitCircle * radius;
            spawnClear = Physics2D.Linecast(gameObject.transform.position, spawnPoint, toHit);
        }

        int roll = Random.Range(0, 5);
        if (roll == 0)
        {
            GameObject powerUp = Instantiate(HPPowerUp, spawnPoint, gameObject.transform.rotation);
            powerUp.transform.parent = gameObject.transform.parent;
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 1)
        {
            GameObject powerUp = Instantiate(DMGPowerUp, spawnPoint, gameObject.transform.rotation);
            powerUp.transform.parent = gameObject.transform.parent;
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 2)
        {
            GameObject powerUp = Instantiate(PlayerSpeedPowerUp, spawnPoint, gameObject.transform.rotation);
            powerUp.transform.parent = gameObject.transform.parent;
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 3)
        {
            GameObject powerUp = Instantiate(RateOfFiePowerUp, spawnPoint, gameObject.transform.rotation);
            powerUp.transform.parent = gameObject.transform.parent;
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 4)
        {
            GameObject powerUp = Instantiate(ProjectileSpeedPowerUp, spawnPoint, gameObject.transform.rotation);
            powerUp.transform.parent = gameObject.transform.parent;
            StartCoroutine(SpawnDelay(powerUp));
        }
    }

    IEnumerator SpawnDelay(GameObject powerUp)
    {
        BoxCollider2D bc = powerUp.GetComponent<BoxCollider2D>();
        bc.enabled = false;
        yield return new WaitForSecondsRealtime(pickUpTime);
        bc.enabled = true;
    }
}
