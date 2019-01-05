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

        GameplayManager.instance.currentRoom = room.North;

        Player.instance.transform.position = GameplayManager.instance.currentRoom.sDoor.GetComponent<SouthDoor>().spawn.transform.position;

    }
}
