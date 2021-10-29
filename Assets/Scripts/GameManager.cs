using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // private singleton instance
    public static GameManager instance = null; // should be able to access from anywhere ?


    public Camera  mainCamera;
    public Vector3 screenBounds;

    public bool  humanPlayer = true;

    // public bool active_game = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        mainCamera   = Camera.main;
        screenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
    }

    public void StartGame()         // Link to Startup.StartGameButton.OnClick
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("Game");
    }

    public void NewGame()           // Link to GameOver.NewGameButton.OnClick
    {
        Debug.Log("New Game");
        SceneManager.LoadScene("Startup");
    }

    public void GameOver()          //
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }

    public void ExitGame()           // Link to Game.ExitGameButton.OnClick
    {
        Debug.Log("Quit Game");
        SceneManager.LoadScene("Startup");
    }

    public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
    {
        Debug.Log("Quit Game");
        // exit game ?
    }
}
