using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int healthIncrease = 1;
    public float pickUpTime = 1.5f;

    private void Start()
    {
        SpawnDelay();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.playerMaxHealth += healthIncrease;
            Player.instance.playerCurHealth += healthIncrease;
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnDelay()
    {
        BoxCollider2D bc = gameObject.GetComponent<BoxCollider2D>();
        bc.enabled = false;
        yield return new WaitForSecondsRealtime(pickUpTime);
        bc.enabled = true;
    }
}
