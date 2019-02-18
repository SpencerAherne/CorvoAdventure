using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections.ObjectModel;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    //TODO: call loot drop roll method once I figure out how to trigger it by clearing room of enemies.
    public GameObject nDoor;
    public GameObject sDoor;
    public GameObject eDoor;
    public GameObject wDoor;

    public Sprite openDoorSprite;

    // Know what's connected it
    // Needs to what objects (barrels, items, enemies)

    public Room North { get; set; }
    public Room South { get; set; }
    public Room East { get; set; }
    public Room West { get; set; }
    public List<Room> Directions { get; set; }

    //x,y coordinates to keep track of room locations reletive to eachother.
    public float XCoord { get; set; }
    public float YCoord { get; set; }
    public Vector2 Coordinates { get; set; }

    public bool treasureRoom = false;

    [SerializeField]
    List<GameObject> objectsInRoom = new List<GameObject>();//Don't think is needed.
    [SerializeField]
    List<GameObject> itemsInRoom = new List<GameObject>();//Don't think is needed.
    [SerializeField]
    public ObservableCollection<GameObject> enemiesInRoom = new ObservableCollection<GameObject>();

    LootDropRolls loot;

    public Room()
    {

    }

    private void Awake()
    {
        Directions.Add(North);
        Directions.Add(South);
        Directions.Add(East);
        Directions.Add(West);
        Coordinates = new Vector2(XCoord, YCoord);
        loot = new LootDropRolls();
    }

    private void Start()
    {
        if (North == null)
        {
            nDoor.SetActive(false);
        }
        if (South == null)
        {
            sDoor.SetActive(false);
        }
        if (East == null)
        {
            eDoor.SetActive(false);
        }
        if (West == null)
        {
            wDoor.SetActive(false);
        }
        if (enemiesInRoom != null && enemiesInRoom.Count > 0)//check that this works as intended, where it only subscribes to OnRoomClear if the room started with enemies in it.
        {
            GameplayManager.OnRoomClear += RoomClear;
        }
    }

    private void Update()
    {
        if (enemiesInRoom == null || enemiesInRoom.Count == 0)
        {
            if (nDoor.activeInHierarchy && nDoor.GetComponent<SpriteRenderer>().sprite != openDoorSprite)
            {
                nDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                nDoor.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            if (sDoor.activeInHierarchy && sDoor.GetComponent<SpriteRenderer>().sprite != openDoorSprite)
            {
                sDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                sDoor.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            if (eDoor.activeInHierarchy && eDoor.GetComponent<SpriteRenderer>().sprite != openDoorSprite)
            {
                eDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                eDoor.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            if (wDoor.activeInHierarchy && wDoor.GetComponent<SpriteRenderer>().sprite != openDoorSprite)
            {
                wDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                wDoor.GetComponent<BoxCollider2D>().isTrigger = true;
            }

        }
    }

    //Make sure this can return null if there are no null sides!
    public string FindNullSide()
    {
        string nullSide;
        List<Room> x = new List<Room>();
        foreach (Room direction in Directions)
        {
            if (direction == null)
            {
                x.Add(direction);
            }
        }
        if (x.Count == 0)
        {
            return null;
        }
        var random = new System.Random();
        int randomInt = random.Next(x.Count);
        nullSide = x[randomInt].ToString();
        return nullSide;
    }

    void RoomClear()
    {
        loot.RoomClearLootRoll();
        GameplayManager.OnRoomClear -= RoomClear;
    }
}
