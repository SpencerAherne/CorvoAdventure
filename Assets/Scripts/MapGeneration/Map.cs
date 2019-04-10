using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Map : MonoBehaviour
{
    List<Room> rooms;
    List<Room> roomPreFabs;
    List<Room> sRoomPreFabs;
    List<Room> tRoomPreFabs;
    List<Room> bRoomPreFabs;
    public List<GameObject> spawnRoomPrefab;
    int roomCount;
    int treasureRoom;
    public List<GameObject> bossRoomPrefabs;
    public List<GameObject> treasureRoomPrefabs;
    List<Room> roomsAdded;
    public List<GameObject> preFabs;
    public Material treasureRoomMat;
    public Sprite closedDoorSprite;

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
        roomPreFabs = new List<Room>();
        sRoomPreFabs = new List<Room>();
        tRoomPreFabs = new List<Room>();
        bRoomPreFabs = new List<Room>();
        rooms = new List<Room>();
        roomsAdded = new List<Room>();
        roomCount = 0;

        foreach (GameObject gameObject in spawnRoomPrefab)
        {
            Room room = Instantiate(gameObject.GetComponent<Room>());
            sRoomPreFabs.Add(room);
        }

        foreach (GameObject gameObject in preFabs)
        {
            Room room = Instantiate(gameObject.GetComponent<Room>());
            roomPreFabs.Add(room);
        }

        foreach (GameObject gameObject in treasureRoomPrefabs)
        {
            Room room = Instantiate(gameObject.GetComponent<Room>());
            tRoomPreFabs.Add(room);
        }

        foreach (GameObject gameObject in bossRoomPrefabs)
        {
            Room room = Instantiate(gameObject.GetComponent<Room>());
            bRoomPreFabs.Add(room);
        }

        var random = new System.Random();
        int index = random.Next(sRoomPreFabs.Count);
        Room spawnRoom = sRoomPreFabs[index].GetComponent<Room>();

        var currentRoom = spawnRoom;
        currentRoom.XCoord = 0;
        currentRoom.YCoord = 0;
        rooms.Add(spawnRoom);
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

            currentRoom = ChangeCurrentRoom();
            Debug.Log("CurrentRoom was changed");
            Debug.Log($"roomCount at end is {roomCount}");
        }

        SpawnBossRoom();

        foreach (Room room in rooms)
        {
            room.gameObject.SetActive(true);
            AddDoors(room);
            room.gameObject.SetActive(false);
        }

        spawnRoom.gameObject.SetActive(true);
        return spawnRoom;
    }

    private Room FindNextRoom(List<Room> PreFabs)
    {
        var random = new System.Random();
        int index = random.Next(PreFabs.Count);
        Room nextRoom = PreFabs[index].GetComponent<Room>();
        PreFabs.Remove(PreFabs[index]);
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
                return null;
            }

            Debug.Log("Does script get here?");
            Room nextRoom;
            if (roomCount == treasureRoom)
            {
                nextRoom = FindNextRoom(tRoomPreFabs);
            }
            else
            {
                nextRoom = FindNextRoom(roomPreFabs);
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
            Room roomToEast = rooms.FirstOrDefault(adjecent => adjecent.XCoord == room.XCoord + 1 && adjecent.YCoord == room.YCoord);
            if (roomToEast != null && roomToEast != currentRoom)
            {
                room.East = roomToEast;
                roomToEast.West = room;
            }
            Room roomToWest = rooms.FirstOrDefault(adjecent => adjecent.XCoord == room.XCoord - 1 && adjecent.YCoord == room.YCoord);
            if (roomToWest != null && roomToWest != currentRoom)
            {
                room.West = roomToWest;
                roomToWest.East = room;
            }
            Room roomToNorth = rooms.FirstOrDefault(adjecent => adjecent.YCoord == room.YCoord + 1 && adjecent.XCoord == room.XCoord);
            if (roomToNorth != null && roomToNorth != currentRoom)
            {
                room.North = roomToNorth;
                roomToNorth.South = room;
            }
            Room roomToSouth = rooms.FirstOrDefault(adjecent => adjecent.YCoord == room.YCoord - 1 && adjecent.XCoord == room.XCoord);
            if (roomToSouth != null && roomToSouth != currentRoom)
            {
                room.South = roomToSouth;
                roomToSouth.North = room;
            }
        }
    }

    private void AddDoors(Room currentRoom)
    {
        if (currentRoom.North == null)
        {
            currentRoom.nDoor.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            currentRoom.nDoor.GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
            currentRoom.nDoor.GetComponent<Renderer>().enabled = true;
            currentRoom.nDoor.GetComponent<NorthDoor>().NorthRoom = currentRoom.North;
            if (currentRoom.North.treasureRoom == true)
            {
                currentRoom.nDoor.GetComponent<SpriteRenderer>().material = treasureRoomMat;
            }
        }
        if (currentRoom.South == null)
        {
            currentRoom.sDoor.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            currentRoom.sDoor.GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
            currentRoom.sDoor.GetComponent<Renderer>().enabled = true;
            currentRoom.sDoor.GetComponent<SouthDoor>().SouthRoom = currentRoom.South;
            if (currentRoom.South.treasureRoom == true)
            {
                currentRoom.sDoor.GetComponent<SpriteRenderer>().material = treasureRoomMat;
            }
        }
        if (currentRoom.East == null)
        {
            currentRoom.eDoor.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            currentRoom.eDoor.GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
            currentRoom.eDoor.GetComponent<Renderer>().enabled = true;
            currentRoom.eDoor.GetComponent<EastDoor>().EastRoom = currentRoom.East;
            if (currentRoom.East.treasureRoom == true)
            {
                currentRoom.eDoor.GetComponent<SpriteRenderer>().material = treasureRoomMat;
            }
        }
        if (currentRoom.West == null)
        {
            currentRoom.wDoor.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            currentRoom.wDoor.GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
            currentRoom.wDoor.GetComponent<Renderer>().enabled = true;
            currentRoom.wDoor.GetComponent<WestDoor>().WestRoom = currentRoom.West;
            if (currentRoom.West.treasureRoom == true)
            {
                currentRoom.wDoor.GetComponent<SpriteRenderer>().material = treasureRoomMat;
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
            if (room.XCoord > xMax)
            {
                xMax = room.XCoord;
                xMaxRoom = room;
            }
            else if (room.XCoord < xMin)
            {
                xMin = room.XCoord;
                xMinRoom = room;
            }

            if (room.YCoord > yMax)
            {
                yMax = room.YCoord;
                yMaxRoom = room;
            }
            else if (room.YCoord < yMin)
            {
                yMin = room.YCoord;
                yMinRoom = room;
            }
        }
        Room bossRoom = FindNextRoom(bRoomPreFabs);

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

        currentRoom = query[sideIndex];

        return currentRoom;
    }
}
