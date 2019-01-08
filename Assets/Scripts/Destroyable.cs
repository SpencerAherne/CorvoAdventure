using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public bool damageFromProjectile;
    public float objMaxHealth = 10;
    public float objCurrentHealth;
    LootDropRolls loot;

    private void Awake()
    {
        loot = GameObject.Find("GamePlayManager").GetComponent<LootDropRolls>();
    }

    // Use this for initialization
    void Start ()
    {
        objCurrentHealth = objMaxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void DamageObject(float damage)
    {
        if (damageFromProjectile)
        {
            objCurrentHealth -= damage;
            if (objCurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        loot.DestroyableLootRoll(gameObject);
    }

}
