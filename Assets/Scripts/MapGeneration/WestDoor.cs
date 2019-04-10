using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestDoor : MonoBehaviour
{
    Room room;
    public GameObject spawn;
    public Room WestRoom { get; set; }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        room = gameObject.GetComponentInParent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //bunch of art and animation stuff
            room.gameObject.SetActive(false);
            WestRoom.gameObject.SetActive(true);

            Player.instance.transform.position = WestRoom.eDoor.GetComponent<EastDoor>().spawn.transform.position;
        }
    }

}
