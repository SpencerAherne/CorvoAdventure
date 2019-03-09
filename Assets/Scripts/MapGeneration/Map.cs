using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Map : MonoBehaviour
{
    List<Room> rooms;
    public GameObject spawnRoomPrefab;
    int roomCount;
    int treasureRoom;
    public List<GameObject> bossRoomPrefabs;
    public List<GameObject> treasureRoomPrefabs;
    List<Room> roomsAdded;
    public List<GameObject> preFabs;

    private void Start()
    {
        MapGen();
        Player.instance.transform.position = GameObject.Find("PlayerSpawn").transform.position;
    }

    public Room MapGen()
    {
        Room currentRoom = BuildMap(preFabs);
        return currentRoom;
    }

    /// <summary>
    /// Builds a map and returns the starting room
    /// </summary>
    /// <param name="preFabs">The PreFabs Rooms</param>
    /// <returns>Starting Room</returns>
    private Room BuildMap(List<GameObject> preFabs)
    {
        rooms = new List<Room>();
        roomsAdded = new List<Room>();
        Room spawnRoom = spawnRoomPrefab.GetComponent<Room>();
        roomCount = 0;

        Debug.Log("Code sets startingRoom");
        Instantiate(spawnRoom.gameObject).SetActive(true);
        var currentRoom = spawnRoom;
        currentRoom.XCoord = 0;
        currentRoom.YCoord = 0;
        rooms.Add(spawnRoom);
        Debug.Log(currentRoom);
        //Pick a random number between 0 and roomcount to be a treasure room.
        treasureRoom = UnityEngine.Random.Range(0, 10);
        while (roomCount < 10)
        {
            Debug.Log($"roomCount at start is {roomCount}");
            Debug.Log(currentRoom);
            GenerateRoomExits(currentRoom);
            Debug.Log("GenerateRoomExits was called correctly");

            CheckAdjacency(currentRoom);
            Debug.Log("CheckAdjeceny was called correctly");

            rooms.AddRange(roomsAdded);
            roomsAdded.Clear();
            Debug.Log("Rooms were correctly added to list");

            Instantiate(currentRoom.gameObject);
            Debug.Log("Room was instatiated");

            currentRoom = ChangeCurrentRoom();
            Debug.Log("CurrentRoom was changed");
            Debug.Log($"roomCount at end is {roomCount}");
        }
        SpawnBossRoom();
        return spawnRoom;
    }

    private Room FindNextRoom(List<GameObject> preFabs)
    {
        var random = new System.Random();
        int index = random.Next(preFabs.Count);
        Room nextRoom = preFabs[index].GetComponent<Room>();
        preFabs.Remove(preFabs[index]);
        return nextRoom;
    }

    private Room GenerateRoomExits(Room currentRoom)
    {
        Debug.Log(currentRoom);
        int numberOfExits = UnityEngine.Random.Range(1, 3);
        for (int i = 0; i < numberOfExits; i++)
        {
            if (currentRoom.FindNullSide() == null)
            {
                Debug.Log("This null is planned, is there way to make not an error?");
                return null;
            }

            Debug.Log("Does script get here?");
            Room nextRoom;
            if (roomCount == treasureRoom)
            {
                nextRoom = FindNextRoom(treasureRoomPrefabs);
            }
            else
            {
                nextRoom = FindNextRoom(preFabs);
            }
            nextRoom.XCoord = currentRoom.XCoord;
            nextRoom.YCoord = currentRoom.YCoord;

            switch (currentRoom.FindNullSide())
            {
                case "North":
                    currentRoom.North = nextRoom;
                    nextRoom.South = currentRoom;
                    nextRoom.YCoord++;
                    break;
                case "South":
                    currentRoom.South = nextRoom;
                    nextRoom.North = currentRoom;
                    nextRoom.YCoord--;
                    break;
                case "East":
                    currentRoom.East = nextRoom;
                    nextRoom.West = currentRoom;
                    nextRoom.XCoord++;
                    break;
                case "West":
                    currentRoom.West = nextRoom;
                    nextRoom.East = currentRoom;
                    nextRoom.XCoord--;
                    break;
            }
            Debug.Log("Does script get here?");
            Debug.Log($"roomcount is {roomCount}");
            roomCount++;
            Debug.Log($"roomcount is {roomCount}");
            roomsAdded.Add(nextRoom);
        }
        return currentRoom;
    }

    private void CheckAdjacency(Room currentRoom)
    {
        foreach (Room room in roomsAdded)
        {
            Room roomToEast = rooms.FirstOrDefault(adjecent => adjecent.Coordinates.x == room.Coordinates.x + 1);
            if (roomToEast != null && roomToEast != currentRoom)
            {
                room.East = roomToEast;
                roomToEast.West = room;
            }
            Room roomToWest = rooms.FirstOrDefault(adjecent => adjecent.Coordinates.x == room.Coordinates.x - 1);
            if (roomToWest != null && roomToWest != currentRoom)
            {
                room.West = roomToWest;
                roomToWest.East = room;
            }
            Room roomToNorth = rooms.FirstOrDefault(adjecent => adjecent.Coordinates.y == room.Coordinates.y + 1);
            if (roomToNorth != null && roomToNorth != currentRoom)
            {
                room.North = roomToNorth;
                roomToNorth.South = room;
            }
            Room roomToSouth = rooms.FirstOrDefault(adjecent => adjecent.Coordinates.y == room.Coordinates.y - 1);
            if (roomToSouth != null && roomToSouth != currentRoom)
            {
                room.South = roomToSouth;
                roomToSouth.North = room;
            }
        }
    }

    private void SpawnBossRoom()
    {
        float xMax = 0;
        float yMax = 0;
        float xMin = 0;
        float yMin = 0;
        Room xMaxRoom = null;
        Room xMinRoom = null;
        Room yMaxRoom = null;
        Room yMinRoom = null;
        foreach (Room room in rooms)
        {
            if (room.Coordinates.x > xMax)
            {
                xMax = room.Coordinates.x;
                xMaxRoom = room;
            }
            else if (room.Coordinates.x < xMin)
            {
                xMin = room.Coordinates.x;
                xMinRoom = room;
            }

            if (room.Coordinates.y > yMax)
            {
                yMax = room.Coordinates.y;
                yMaxRoom = room;
            }
            else if (room.Coordinates.y < yMin)
            {
                yMin = room.Coordinates.y;
                yMinRoom = room;
            }
        }
        Room bossRoom = FindNextRoom(bossRoomPrefabs);

        List<string> highLowXY = new List<string>();

        if (xMaxRoom != null)
        {
            highLowXY.Add("xMax");
        }

        if (xMinRoom != null)
        {
            highLowXY.Add("xMin");
        }

        if (yMaxRoom != null)
        {
            highLowXY.Add("yMax");
        }

        if (yMinRoom != null)
        {
            highLowXY.Add("yMin");
        }


        var randomBossRoom = new System.Random();
        int bossRoomIndex = randomBossRoom.Next(highLowXY.Count);
        string bossRoomLoc = highLowXY[bossRoomIndex];

        switch (bossRoomLoc.ToString())
        {
            case "xMax":
                bossRoom.West = xMaxRoom;
                bossRoom.XCoord = xMaxRoom.XCoord + 1;
                bossRoom.YCoord = xMaxRoom.YCoord;
                xMaxRoom.East = bossRoom;
                break;
            case "xMin":
                bossRoom.East = xMinRoom;
                bossRoom.XCoord = xMinRoom.XCoord - 1;
                bossRoom.YCoord = xMinRoom.YCoord;
                xMinRoom.West = bossRoom;
                break;
            case "yMax":
                bossRoom.South = yMaxRoom;
                bossRoom.XCoord = yMaxRoom.XCoord;
                bossRoom.YCoord = yMaxRoom.YCoord + 1;
                yMaxRoom.North = bossRoom;
                break;
            case "yMin":
                bossRoom.North = yMinRoom;
                bossRoom.XCoord = yMinRoom.XCoord;
                bossRoom.YCoord = yMinRoom.YCoord - 1;
                yMinRoom.South = bossRoom;
                break;
        }
        rooms.Add(bossRoom);
    }

    private Room ChangeCurrentRoom()
    {
        Room currentRoom;

        List<Room> query = rooms.Where(blank => blank.North == null || blank.South == null || blank.East == null || blank.West == null).ToList();

        //selects a random room from the list
        var randomSide = new System.Random();
        int sideIndex = randomSide.Next(query.Count());

        currentRoom = query.Take(sideIndex) as Room;

        return currentRoom;
    }
}
