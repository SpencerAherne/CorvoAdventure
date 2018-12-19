using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public Sprite openChest;
    public GameObject HP;
    public GameObject DMG;
    public GameObject ROF;
    public GameObject PS;
    public GameObject SPD;

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
            Instantiate(HP, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 1)
        {
            Instantiate(DMG, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 2)
        {
            Instantiate(SPD, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 3)
        {
            Instantiate(ROF, -player.transform.position, gameObject.transform.rotation);
        }
        if (roll == 4)
        {
            Instantiate(PS, -player.transform.position, gameObject.transform.rotation);
        }
    }
}
