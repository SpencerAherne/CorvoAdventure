using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public bool damageFromProjectile;
    public float objMaxHealth = 10;
    public float objCurrentHealth;
    GameObject Key;
    GameObject Gem;
    GameObject Potion;


	// Use this for initialization
	void Start ()
    {
        objCurrentHealth = objMaxHealth;
        Key = GameObject.Find("KeyPool");
        Potion = GameObject.Find("PotionPool");
        Gem = GameObject.Find("GemPool");
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
        DropItem();
    }

    void DropItem()
    {
        int roll = Random.Range(1, 101);
        if (roll <= 5)
        {
            GameObject clone = Key.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        if (roll > 5 && roll <= 10)
        {
            GameObject clone = Potion.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        if (roll > 10 && roll <= 20)
        {
            GameObject clone = Gem.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        if (roll > 20)
        {
            Debug.Log("The loot system rolled correctly");
        }
    }
}
