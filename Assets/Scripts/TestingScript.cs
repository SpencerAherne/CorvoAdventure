﻿using System.Collections;
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

    private Room BuildMap(List<GameObject> preFabs)
    {
        _rooms = new List<Room>();
        blankSides = new List<Room>();
        nullSide = new List<Room>();
        roomsAdded = new List<Room>();
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
            int numberOfExits = UnityEngine.Random.Range(1, 3);
            for (int i = 0; i < numberOfExits; i++)
            {
                if (currentRoom.FindNullSide() == null)
                {
                    break;
                }
                var nextRoom = FindNextRoom(preFabs);
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


            _rooms.AddRange(roomsAdded);

            IEnumerable<Room> query = _rooms.Where(blank => blank.North == null || blank.South == null || blank.East == null || blank.West == null);

            //selects a random room from the list
            var randomSide = new System.Random();
            int sideIndex = randomSide.Next(query.Count());


            currentRoom = query.Take(sideIndex) as Room;

            roomsAdded.Clear();

        }

        //CHANGE ALL OF THIS(refarctoring I think)
        //randomly pick single highest or lowest x or y and continue in that direction.
        float xMax = 0;
        float yMax = 0;
        float xMin = 0;
        float yMin = 0;
        foreach (Room room in _rooms)
        {
            if (room.Coordinates.x > xMax)
            {
                xMax = room.Coordinates.x;
            }
            else if (room.Coordinates.x < xMin)
            {
                xMin = room.Coordinates.x;
            }

            if(room.Coordinates.y > yMax)
            {
                yMax = room.Coordinates.y;
            }
            else if (room.Coordinates.y < yMin)
            {
                yMin = room.Coordinates.y;
            }
        }
        //Should I add the boss room to the rooms list?
        Room bossRoom = FindNextRoom(bossRoomPrefabs);

        Vector2 highestRoom = new Vector2(xMax, yMax);
        Vector2 lowestRoom = new Vector2(xMin, yMin);


        List<Vector2> highOrLow = new List<Vector2>
        {
            highestRoom,
            lowestRoom
        };

        var randomBossRoom = new System.Random();
        int bossRoomIndex = randomBossRoom.Next(highOrLow.Count);
        Vector2 bossRoomLoc = highOrLow[bossRoomIndex];

        List<float> high = new List<float>();
        List<float> low = new List<float>();
        high.Add(highestRoom.x);
        high.Add(highestRoom.y);
        low.Add(lowestRoom.x);
        low.Add(lowestRoom.y);

        if (highOrLow[bossRoomIndex] == highestRoom)
        {

            Room query = _rooms.Where(room => room.Coordinates == highestRoom) as Room;

            var random = new System.Random();
            int xOrY = random.Next(high.Count);

            float direction = high[xOrY];
            if (direction == highestRoom.x)
            {
                bossRoom.Coordinates = new Vector2(highestRoom.x + 1, highestRoom.y);
                bossRoom.West = query;
                query.East = bossRoom;
            }
            else
            {
                bossRoom.Coordinates = new Vector2(highestRoom.x, highestRoom.y + 1);
                bossRoom.South = query;
                query.North = bossRoom;
            }
        }
        else
        {

            Room query = _rooms.Where(room => room.Coordinates == lowestRoom) as Room;

            var random = new System.Random();
            int xOrY = random.Next(low.Count);

            float direction = low[xOrY];
            if (direction == lowestRoom.x)
            {
                bossRoom.Coordinates = new Vector2(highestRoom.x - 1, highestRoom.y);
                bossRoom.East = query;
                query.West = bossRoom;
            }
            else
            {
                bossRoom.Coordinates = new Vector2(highestRoom.x, highestRoom.y - 1);
                bossRoom.North = query;
                query.South = bossRoom;
            }
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
}
