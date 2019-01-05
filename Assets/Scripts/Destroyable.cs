using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public bool damageFromProjectile;
    public float objMaxHealth = 10;
    public float objCurrentHealth;
    LootDropRolls loot;


	// Use this for initialization
	void Start ()
    {
        objCurrentHealth = objMaxHealth;
        loot = new LootDropRolls();
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
        loot.DestroyableLootRoll();
    }

}
