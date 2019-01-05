using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Room : MonoBehaviour
{

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
    public List<GameObject> enemiesInRoom = new List<GameObject>();

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
    }

    private void Update()
    {
        if (enemiesInRoom.Count == 0 || enemiesInRoom == null)//Does this keep setting the sprite, if so how to only call once?
        {
            if (nDoor.activeInHierarchy)
            {
                nDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            }
            if (sDoor.activeInHierarchy)
            {
                sDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            }
            if (eDoor.activeInHierarchy)
            {
                eDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            }
            if (wDoor.activeInHierarchy)
            {
                wDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            }

            if (enemiesInRoom.Count == 0)//want this to only happen when room is cleared by killing enemies, not on empty rooms.
            {
                loot.RoomClearLootRoll();
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
}
