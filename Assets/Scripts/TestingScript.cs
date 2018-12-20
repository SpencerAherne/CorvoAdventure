using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    int roomCount;
    List<Room> _rooms;
    List<GameObject> bossRoomPrefabs; //I don't get why this needs to be GameObjects when prefabs can be Rooms
    List<Room> blankSides;
    List<Room> nullSide;
    List<Room> roomsAdded;
    List<GameObject> roomPrefabs = new List<GameObject>();


    private Room BuildMap(List<GameObject> preFabs)
    {
        _rooms = new List<Room>();
        blankSides = new List<Room>();
        nullSide = new List<Room>();
        roomsAdded = new List<Room>();
        bossRoomPrefabs = new List<GameObject>();
        roomCount = 0;


        var startingRoom = FindNextRoom(preFabs); // this will be a specific prefab starting room;

        //GenerateStartRoom(startingRoom);


        var currentRoom = startingRoom;
        currentRoom.XCoord = 0;
        currentRoom.YCoord = 0;
        _rooms.Add(startingRoom);


        // TODO: Develop room linking algorithim.


        while (roomCount < 10)
        {
            GenerateRoomExits(currentRoom);

            CheckAdjacency(currentRoom);

            _rooms.AddRange(roomsAdded);
            roomsAdded.Clear();

            IEnumerable<Room> query = _rooms.Where(blank => blank.North == null || blank.South == null || blank.East == null || blank.West == null);

            //selects a random room from the list
            var randomSide = new System.Random();
            int sideIndex = randomSide.Next(query.Count());

            currentRoom = query.Take(sideIndex) as Room;
        }

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

            if(room.Coordinates.y > yMax)
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
        //Should I add the boss room to the rooms list?
        Room bossRoom = FindNextRoom(bossRoomPrefabs);

        List<float> highLowXY = new List<float>
        {
            xMax,
            xMin,
            yMax,
            yMin
        };

        var randomBossRoom = new System.Random();
        int bossRoomIndex = randomBossRoom.Next(highLowXY.Count);
        float bossRoomLoc = highLowXY[bossRoomIndex];

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

        return startingRoom;
    }

    private Room FindNextRoom(List<GameObject> preFabs)
    {
        // TODO: randomly pick a new room from the preFabs, setting any variables as required
        // Consider removing already selected rooms, or room selected more than twice.


        //currently returns Room script from the prefab gameobject.
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
                break;
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
                Room eastRoom = roomToEast;
                room.East = eastRoom;
                eastRoom.West = room;
            }
            Room roomToWest = _rooms.First(adjecent => adjecent.Coordinates.x == room.Coordinates.x - 1);
            if (roomToWest != null && roomToWest != currentRoom)
            {
                Room westRoom = roomToWest;
                room.West = westRoom;
                westRoom.East = room;
            }
            Room roomToNorth = _rooms.First(adjecent => adjecent.Coordinates.y == room.Coordinates.y + 1);
            if (roomToNorth != null && roomToNorth != currentRoom)
            {
                Room northRoom = roomToNorth;
                room.North = northRoom;
                northRoom.South = room;
            }
            Room roomToSouth = _rooms.First(adjecent => adjecent.Coordinates.y == room.Coordinates.y - 1);
            if (roomToSouth != null && roomToSouth != currentRoom)
            {
                Room southRoom = roomToSouth;
                room.South = southRoom;
                southRoom.North = room;
            }
        }

    }
}
