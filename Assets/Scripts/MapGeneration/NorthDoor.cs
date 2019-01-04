using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthDoor : MonoBehaviour
{
    Room room;
    public GameObject spawn;

    void Start()
    {
        room = GetComponentInParent<Room>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //whole bunch of animation and art stuff for changing rooms
        room.gameObject.SetActive(false);
        room.North.gameObject.SetActive(true);

        Player.instance.currentRoom = room.North; //or would this also go in room logic? Should this just be moving the player and that's it?

        Player.instance.transform.position = spawn.transform.position;
    }
}
