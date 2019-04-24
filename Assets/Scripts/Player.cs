using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;

    public static Player instance;

    public int keyCount;
    public int gemCount;
    public int arcanePulseCount;
    public float playerMaxHealth;
    public float playerTotalHealth;
    public float playerStartHealth;
    public float playerCurHealth;
    public float playerSpeed = 10f;
    public float playerDamage;
    public float playerFireRate;
    public float playerProjectileSpeed;
    public Room currentRoom;

    public bool isDebuffed = false;

    public GameObject arcanePulse;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        playerMaxHealth = 10f;
        playerTotalHealth = playerStartHealth;
        playerCurHealth = playerStartHealth;
        playerMovement = GetComponent<PlayerMovement>();
	}

    private void Update()
    {
        playerMovement.Move();
        UseArcanePulse();

        if (playerTotalHealth > playerMaxHealth)
        {
            playerTotalHealth = playerMaxHealth;
        }

        if (GelatinousCubeBoss.instance.trail.bounds.Contains(transform.position) && isDebuffed == false)
        {
            StartCoroutine(SlowDebuff());
        }
    }

    void FixedUpdate ()
    {
        if (playerCurHealth > playerTotalHealth)
        {
            playerCurHealth = playerTotalHealth;
        }
	}

    public void DamagePlayer(float damage)
    {
        playerCurHealth -= damage;
        if (playerCurHealth <= 0)
        {
            Time.timeScale = 0f;
            GameplayManager.instance.gameOverScreen.SetActive(true);
        }
        //give invul frames
    }

    void UseArcanePulse()
    {
        if (Input.GetKeyDown(KeyCode.Q) && arcanePulseCount > 0)
        {
            Debug.Log("UseArcanePulse was called");
            GameObject clone = GameObject.Find("ArcanePulsePool").GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = transform.position;
            clone.SetActive(true);
            arcanePulseCount -= 1;
        }
    }

    public IEnumerator SlowDebuff()
    {
        isDebuffed = true;
        playerSpeed = playerSpeed * GelatinousCubeBoss.instance.slowRate;
        yield return new WaitForSecondsRealtime(GelatinousCubeBoss.instance.debuffDuration);
        playerSpeed = playerSpeed / GelatinousCubeBoss.instance.slowRate;
        isDebuffed = false;
    }
}
