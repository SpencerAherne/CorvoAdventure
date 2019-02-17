using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour {

    public int healthHealed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "Key":
                    Player.instance.keyCount++;
                    gameObject.SetActive(false);
                    break;
                case "Potion":
                    if (Player.instance.playerCurHealth < Player.instance.playerMaxHealth)
                    {
                        Player.instance.playerCurHealth += healthHealed;
                        gameObject.SetActive(false);
                    }
                    break;
                case "Gem":
                    Player.instance.gemCount++;
                    gameObject.SetActive(false);
                    break;
                case "Scroll":
                    {
                        Player.instance.arcanePulseCount++;
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}
