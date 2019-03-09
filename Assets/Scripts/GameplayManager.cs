using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    #region Singleton
    public static GameplayManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Room currentRoom;
    public delegate void RoomCleared();
    public static event RoomCleared OnRoomClear;
    
    // Start is called before the first frame update
    void Start()
    {

        //Figure out how to call map method
        //spawn player in starting room
    }

    // Update is called once per frame
    void Update()
    {
        //keep track of current room for other components to check against.
        //Should only be called when a room has had enemies, but no longer does. Not sure if works as I intend, also need to make sure the current room is tracked properly.
        if (currentRoom.enemiesInRoom.Count == 0)
        {
            OnRoomClear?.Invoke();
        }
    }

}
