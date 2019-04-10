using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthDoor : MonoBehaviour
{
    Room room;
    public GameObject spawn;
    public Room SouthRoom { get; set; }

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
            SouthRoom.gameObject.SetActive(true);

            Player.instance.transform.position = SouthRoom.nDoor.GetComponent<NorthDoor>().spawn.transform.position;
        }
    }
}
