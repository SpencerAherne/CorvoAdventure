﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastDoor : MonoBehaviour
{
    Room room;
    public GameObject spawn;
    SpriteRenderer spriteRenderer;
    public Material treasureRoomMat;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        room = gameObject.GetComponentInParent<Room>();
        if (room.East.treasureRoom == true)
        {
            spriteRenderer.material = treasureRoomMat;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bunch of art and animation stuff
        room.gameObject.SetActive(false);
        room.East.gameObject.SetActive(true);

        GameplayManager.instance.currentRoom = room.East;

        Player.instance.transform.position = GameplayManager.instance.currentRoom.wDoor.GetComponent<WestDoor>().spawn.transform.position;
    }

}
