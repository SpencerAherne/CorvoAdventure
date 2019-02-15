using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour {

    public int healthHealed;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (gameObject.tag)
        {
            case "Key":
                if (collision.gameObject.tag == "Player")
                {
                    Player.instance.keyCount++;
                    gameObject.SetActive(false);
                }
                break;

            case "Potion":
                if (collision.gameObject.tag == "Player")
                {
                    if (Player.instance.playerCurHealth < Player.instance.playerMaxHealth)
                    {
                        Player.instance.playerCurHealth += healthHealed;
                        gameObject.SetActive(false);
                    }
                }
                break;

            case "Gem":
                if (collision.gameObject.tag == "Player")
                {
                    Player.instance.gemCount++;
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
