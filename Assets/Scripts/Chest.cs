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
        int roll = Random.Range(0, 5);
        if (roll == 0)
        {
            GameObject powerUp = Instantiate(HPPowerUp, (Vector2)gameObject.transform.position + Random.insideUnitCircle * radius, gameObject.transform.rotation);
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 1)
        {
            GameObject powerUp = Instantiate(DMGPowerUp, (Vector2)gameObject.transform.position + Random.insideUnitCircle * radius, gameObject.transform.rotation);
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 2)
        {
            GameObject powerUp = Instantiate(PlayerSpeedPowerUp, (Vector2)gameObject.transform.position + Random.insideUnitCircle * radius, gameObject.transform.rotation);
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 3)
        {
            GameObject powerUp = Instantiate(RateOfFiePowerUp, (Vector2)gameObject.transform.position + Random.insideUnitCircle * radius, gameObject.transform.rotation);
            StartCoroutine(SpawnDelay(powerUp));
        }
        else if (roll == 4)
        {
            GameObject powerUp = Instantiate(ProjectileSpeedPowerUp, (Vector2)gameObject.transform.position + Random.insideUnitCircle * radius, gameObject.transform.rotation);
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
