using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    List<Room> _rooms;
    List<string> _exits;
    public GameObject startingRoom;

    private List<GameObject> preFabs;

    public Map()
    {
        var preFabs = new List<GameObject>();

        var currentRoom = BuildMap(preFabs);
    }

    /// <summary>
    /// Builds a map and returns the starting room
    /// </summary>
    /// <param name="preFabs">The PreFabs Rooms</param>
    /// <returns>Starting Room</returns>
    private Room BuildMap(List<GameObject> preFabs)
    {
        _rooms = new List<Room>();

        var startingRoom = FindNextRoom(preFabs);

        GenerateStartRoom(startingRoom);

        var currentRoom = startingRoom;
        _rooms.Add(startingRoom);

        // Maps are always 10 rooms, but could be random
        for (int i = 0; i < 10; i++)
        {
            var nextRoom = FindNextRoom(preFabs);

            // TODO: Develop room linking algorithim.
            // Below is sample code for adding a room, linked to each direction

            // North
            currentRoom.North = nextRoom;

            // South
            currentRoom.South = nextRoom;

            // East
            currentRoom.East = nextRoom;

            // West
            currentRoom.West = nextRoom;

            // end of sample code

            currentRoom = nextRoom;
        }

        return startingRoom;
    }

    /// <summary>
    /// Picks a new random room from the list of prefabs. Maybe without replacement?
    /// </summary>
    /// <param name="preFabs"></param>
    /// <returns></returns>
    private Room FindNextRoom(List<GameObject> preFabs)
    {
        // TODO: randomly pick a new room from the preFabs, setting any variables as required
        // Consider removing already selected rooms, or room selected more than twice.

        return null;
    }

    private void GenerateStartRoom(Room room)
    {
        _exits = new List<string>();
        _exits.Add("North");
        _exits.Add("South");
        _exits.Add("East");
        _exits.Add("West");


        //FIGURE OUT HOW TO HAVE ROOMS KEEP THIS INFORMATION
        int numberOfExits = UnityEngine.Random.Range(1, 5);
        for (int i = 0; i < numberOfExits; i++)
        {
            var nextRoom = FindNextRoom(preFabs);
            //picks a random cardinal direction based on amount of exits randomly picked
            var random = new System.Random();
            Debug.Log(random);
            int index = random.Next(_exits.Count);
            Debug.Log(_exits[index]);

            switch (_exits[index])
            {
                case "North":
                    room.North = nextRoom;
                    //activate north door
                    break;
                case "South":
                    room.South = nextRoom;
                    //activate south door
                    break;
                case "East":
                    room.East = nextRoom;
                    //activate east door
                    break;
                case "West":
                    room.West = nextRoom;
                    //activate west door
                    break;
            }

            _exits.Remove(_exits[index]);
        }

    }
}
