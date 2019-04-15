using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropRolls : MonoBehaviour
{
    GameObject Key;
    GameObject Gem;
    GameObject Potion;
    GameObject Chest;
    GameObject Scroll;

    //singleton or reference in gameplaymanager

    private void Awake()
    {
        Key = GameObject.Find("KeyPool");
        Potion = GameObject.Find("PotionPool");
        Gem = GameObject.Find("GemPool");
        Chest = GameObject.Find("Chest");
        Scroll = GameObject.Find("ScrollPool");
    }

    public void DestroyableLootRoll(GameObject gameObject)
    {
        int roll = Random.Range(1, 101);
        if (roll <= 5)
        {
            GameObject clone = Key.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 5 && roll <= 10)
        {
            GameObject clone = Potion.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 10 && roll <= 15)
        {
            GameObject clone = Scroll.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 15 && roll <= 25)
        {
            GameObject clone = Gem.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = gameObject.transform.parent;
            clone.transform.position = gameObject.transform.position;
        }
        else if (roll > 25)
        {
            Debug.Log("The loot system rolled correctly");
        }
    }

    //Currently set to spawn at 0,0,0 best option is to get current room and spawn in the center of it.
    //Set spawnpoint gameobject in rooms, pass in the transform of the room's lootspawn.
    public void RoomClearLootRoll(GameObject lootspawn)
    {
        int roll = Random.Range(1, 101);
        if (roll <= 5)
        {
            GameObject clone = Chest.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = lootspawn.transform;
            clone.transform.position = lootspawn.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 5 && roll <= 15)
        {
            GameObject clone = Key.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = lootspawn.transform;
            clone.transform.position = lootspawn.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 15 && roll <= 25)
        {
            GameObject clone = Scroll.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = lootspawn.transform;
            clone.transform.position = lootspawn.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 25 && roll <= 40)
        {
            GameObject clone = Potion.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = lootspawn.transform;
            clone.transform.position = lootspawn.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 40 && roll <= 70)
        {
            GameObject clone = Gem.GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.parent = lootspawn.transform;
            clone.transform.position = lootspawn.transform.position;
            clone.SetActive(true);
        }
        else if (roll > 70)
        {
            Debug.Log("RoomLoot rolled correctly.");
        }

    }

    public void ChestLootRoll()
    {
        //do I want this?
    }

    public void SkeletonLootRoll()
    {
        //figure out what, if anything, skeletons should drop on death/disable
    }

    public void GoblinLootRoll()
    {
        //figure out what, if anything, goblins should drop on death/disable
    }
}
