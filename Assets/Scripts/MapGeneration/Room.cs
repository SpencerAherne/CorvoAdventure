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

    public bool roomIsClear = false;
    public bool treasureRoom = false;

    [SerializeField]
    List<GameObject> objectsInRoom = new List<GameObject>();
    [SerializeField]
    List<GameObject> itemsInRoom = new List<GameObject>();
    [SerializeField]
    List<GameObject> enemiesInRoom = new List<GameObject>();

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
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (enemiesInRoom.Count == 0)
        {
            roomIsClear = true;
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
        var random = new System.Random();
        int randomInt = random.Next(x.Count);
        nullSide = x[randomInt].ToString();
        return nullSide;
    }
}
