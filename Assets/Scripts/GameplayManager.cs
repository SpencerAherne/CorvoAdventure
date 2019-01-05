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

    // Start is called before the first frame update
    void Start()
    {
        //spawn player in starting room
    }

    // Update is called once per frame
    void Update()
    {
        //keep track of current room for many seperate components to check against.
    }
}
