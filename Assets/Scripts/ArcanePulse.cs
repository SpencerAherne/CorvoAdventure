using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcanePulse : MonoBehaviour
{

    public float areaOfEffect = 1f;
    public LayerMask toHit;
    public float damage = 10f;
    public float timeToExplode = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Pulse", timeToExplode);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, areaOfEffect);
    }

    void Pulse()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, toHit);
        foreach (Collider2D collider in targets)
        {
            switch (collider.tag)
            {
                case "Player":
                    {
                        collider.gameObject.GetComponent<Player>().DamagePlayer(damage);
                        Destroy(gameObject);
                    }
                    break;
                case "Skeleton":
                    {
                        collider.gameObject.GetComponent<Skeleton>().DamageSkeleton(damage);
                        Destroy(gameObject);
                    }
                    break;
                case "Goblin":
                    {
                        collider.gameObject.GetComponent<Goblin>().DamageGoblin(damage);
                        Destroy(gameObject);
                    }
                    break;
                case "Destroyable":
                    {
                        collider.gameObject.GetComponent<Destroyable>().DamageObject(damage);
                        Destroy(gameObject);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
