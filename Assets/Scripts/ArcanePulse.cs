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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        StartCoroutine("Pulse");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, areaOfEffect);
    }

    IEnumerator Pulse() //Gets called even when not using an Arcane Pulse?
    {
        Debug.Log("pulse has started");
        yield return new WaitForSecondsRealtime(timeToExplode);
        Debug.Log("Got past waitforsecondsrealtime");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, toHit);
        foreach (Collider2D collider in targets)
        {
            switch (collider.tag)
            {
                case "Player":
                    {
                        collider.gameObject.GetComponent<Player>().DamagePlayer(damage);
                    }
                    break;
                case "Skeleton":
                    {
                        collider.gameObject.GetComponent<Skeleton>().DamageSkeleton(damage);
                    }
                    break;
                case "Goblin":
                    {
                        collider.gameObject.GetComponent<Goblin>().DamageGoblin(damage);
                    }
                    break;
                case "Destroyable":
                    {
                        collider.gameObject.GetComponent<Destroyable>().DamageObject(damage);
                    }
                    break;
                case "ArmorStand":
                    {
                        collider.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
