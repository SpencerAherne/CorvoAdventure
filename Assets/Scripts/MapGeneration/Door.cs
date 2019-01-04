using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite openDoor;
    public Transform spawnPoint;
    
    Room room;

    bool roomIsClear;

	// Use this for initialization
	void Start ()
    {
        room = gameObject.GetComponentInParent<Room>();
        gameObject.transform.parent = room.transform;
    }

    // Update is called once per frame
    void Update ()
    {

        if (room.roomIsClear == true)
        {
            spriteRenderer.sprite = openDoor;
        }

        if (room.treasureRoom == true)
        {

        }
	}
}
