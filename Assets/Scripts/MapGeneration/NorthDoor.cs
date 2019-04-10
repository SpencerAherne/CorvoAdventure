using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthDoor : MonoBehaviour
{
    Room room;
    public GameObject spawn;
    public Room NorthRoom { get; set; }

    private void Awake()
    {

    }

    void Start()
    {
        room = gameObject.GetComponentInParent<Room>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //whole bunch of animation and art stuff for changing rooms
            room.gameObject.SetActive(false);
            NorthRoom.gameObject.SetActive(true);

            Player.instance.transform.position = NorthRoom.sDoor.GetComponent<SouthDoor>().spawn.transform.position;
        }
    }
}
