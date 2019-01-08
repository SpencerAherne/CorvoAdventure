using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropRolls : MonoBehaviour
{
    GameObject Key;
    GameObject Gem;
    GameObject Potion;
    GameObject Chest;


    private void Awake()
    {
        Key = GameObject.Find("KeyPool");
        Potion = GameObject.Find("PotionPool");
        Gem = GameObject.Find("GemPool");
        Chest = GameObject.Find("Chest");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyableLootRoll(GameObject gameObject)
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

    public void RoomClearLootRoll()
    {
        int roll = Random.Range(1, 101);
        if (roll <= 5)
        {
            GameObject clone = Chest.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        if (roll > 5 && roll <= 15)
        {
            GameObject clone = Key.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        if (roll > 15 && roll <= 30)
        {
            GameObject clone = Potion.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        if (roll > 30 && roll <= 60)
        {
            GameObject clone = Gem.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        if (roll > 60)
        {
            Debug.Log("RoomLoot rolled correctly.");
        }

    }
}
