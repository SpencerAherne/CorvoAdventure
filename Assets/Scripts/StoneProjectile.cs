using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProjectile : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                {
                    collision.gameObject.GetComponent<Player>().DamagePlayer(Goblin.instance.damage);
                    gameObject.SetActive(false);
                }
                break;
            case "Destroyable":
                {
                    collision.gameObject.GetComponent<Destroyable>().DamageObject(Player.instance.playerDamage);
                    gameObject.SetActive(false);
                }
                break;
            default:
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
