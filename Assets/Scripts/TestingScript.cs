using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    int roomCount;
    List<Room> _rooms;
    List<GameObject> bossRoomPrefabs;
    List<Room> roomsAdded;
    List<GameObject> roomPrefabs = new List<GameObject>();


    private Room BuildMap(List<GameObject> preFabs)
    {
        _rooms = new List<Room>();
        roomsAdded = new List<Room>();
        bossRoomPrefabs = new List<GameObject>();
        roomCount = 0;

        var startingRoom = FindNextRoom(preFabs); // this will be a specific prefab starting room;
        //GenerateStartRoom(startingRoom);
        var currentRoom = startingRoom;
        currentRoom.XCoord = 0;
        currentRoom.YCoord = 0;
        _rooms.Add(startingRoom);

        while (roomCount < 10)
        {
            GenerateRoomExits(currentRoom);

            CheckAdjacency(currentRoom);

            _rooms.AddRange(roomsAdded);
            roomsAdded.Clear();

            currentRoom = ChangeCurrentRoom();
        }
        SpawnBossRoom();
        return startingRoom;
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
        int numberOfExits = UnityEngine.Random.Range(1, 3);
        for (int i = 0; i < numberOfExits; i++)
        {
            if (currentRoom.FindNullSide() == null)
            {
                return null;
            }
            var nextRoom = FindNextRoom(roomPrefabs);
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
            roomCount++;
            roomsAdded.Add(nextRoom);
        }
        return currentRoom;
    }

    private void CheckAdjacency(Room currentRoom)
    {
        foreach (Room room in roomsAdded)
        {
            Room roomToEast = _rooms.First(adjecent => adjecent.Coordinates.x == room.Coordinates.x + 1);
            if (roomToEast != null && roomToEast != currentRoom)
            {
                room.East = roomToEast;
                roomToEast.West = room;
            }
            Room roomToWest = _rooms.First(adjecent => adjecent.Coordinates.x == room.Coordinates.x - 1);
            if (roomToWest != null && roomToWest != currentRoom)
            {
                room.West = roomToWest;
                roomToWest.East = room;
            }
            Room roomToNorth = _rooms.First(adjecent => adjecent.Coordinates.y == room.Coordinates.y + 1);
            if (roomToNorth != null && roomToNorth != currentRoom)
            {
                room.North = roomToNorth;
                roomToNorth.South = room;
            }
            Room roomToSouth = _rooms.First(adjecent => adjecent.Coordinates.y == room.Coordinates.y - 1);
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
        foreach (Room room in _rooms)
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

        List<string> highLowXY = new List<string>
        {
            "xMax",
            "xMin",
            "yMax",
            "yMin"
        };

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
        _rooms.Add(bossRoom);
    }

    private Room ChangeCurrentRoom()
    {
        Room currentRoom;

        IEnumerable<Room> query = _rooms.Where(blank => blank.North == null || blank.South == null || blank.East == null || blank.West == null);

        //selects a random room from the list
        var randomSide = new System.Random();
        int sideIndex = randomSide.Next(query.Count());

        currentRoom = query.Take(sideIndex) as Room;

        return currentRoom;
    }
}
