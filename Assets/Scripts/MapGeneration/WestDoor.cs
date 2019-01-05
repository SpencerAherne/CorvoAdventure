using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestDoor : MonoBehaviour
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
        if (room.West.treasureRoom == true)
        {
            spriteRenderer.material = treasureRoomMat;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //bunch of art and animation stuff
        room.gameObject.SetActive(false);
        room.West.gameObject.SetActive(true);

        GameplayManager.instance.currentRoom = room.West;

        Player.instance.transform.position = GameplayManager.instance.currentRoom.eDoor.GetComponent<EastDoor>().spawn.transform.position;
    }

}
