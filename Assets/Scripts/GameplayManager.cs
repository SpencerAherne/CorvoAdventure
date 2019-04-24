using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    #region Singleton
    public static GameplayManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Room CurrentRoom { get; set; }
    public delegate void RoomCleared();
    public static event RoomCleared OnRoomClear;

    //serialization to keep track of these numbers and use them on victory screen.
    public int bossKillCount;
    public int totalGemCount;

    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Should only be called when a room has had enemies, but no longer does. Not sure if works as I intend, also need to make sure the current room is tracked properly.
        if (CurrentRoom.enemiesInRoom.Count == 0)
        {
            OnRoomClear?.Invoke();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
