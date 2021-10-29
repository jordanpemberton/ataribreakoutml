using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // private singleton instance
    public static GameManager instance = null; // should be able to access from anywhere ?

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

    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        Debug.Log("Start New Game");
        SceneManager.LoadScene("Breakout");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }

    public void GameWin()
    {
        Debug.Log("You Win!");
        SceneManager.LoadScene("Victory");
    }

    public void ExitGame()           // Link to Game.ExitGameButton.OnClick
    {
        Debug.Log("Exit Game");
        SceneManager.LoadScene("Startup");
    }

    public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
    {
        Debug.Log("Exit Game");
        // exit game ?
    }
}
