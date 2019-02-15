using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{


    private void Start()
    {

    }

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
            case "Skelton":
                {
                    collision.gameObject.GetComponent<Skeleton>().DamageSkeleton(Player.instance.playerDamage);
                    gameObject.SetActive(false);
                }
                break;
            case "Goblin":
                {
                    collision.gameObject.GetComponent<Goblin>().DamageGoblin(Player.instance.playerDamage);
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
