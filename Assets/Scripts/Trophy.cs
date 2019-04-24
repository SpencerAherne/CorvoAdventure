using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            GameplayManager.instance.victoryScreen.SetActive(true);
        }
    }
}
