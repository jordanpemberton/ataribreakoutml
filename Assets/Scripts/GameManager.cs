using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    public GameObject humanGameManager;
    public GameObject aiGameManager;
    
    public int humanScore = 0;
    public int aiScore = 0;
    public int numTriesPerGame = 1;
    public int gameCount = 0;

    // private int _winner; // 0 = none, 1 = human, 2 = AI
    private bool _paused;
    
    public void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    
    public void NewGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        Debug.Log("Start New Game");
        
        gameCount = 0;
        
        SceneManager.LoadScene("Breakout");
        
        // trigger game manager's start game functions here...?
        if (humanGameManager == null) humanGameManager = GameObject.Find("HumanGameManager");
        if (humanGameManager) humanGameManager.GetComponent<IndvGameManager>().StartGame();
        
        if (aiGameManager == null) aiGameManager = GameObject.Find("AIGameManager");
        if (aiGameManager) aiGameManager.GetComponent<IndvGameManager>().StartGame();
    }
    
    public void GameOver()
    {
        // give some num tries first...?
        gameCount++;
        if (gameCount != numTriesPerGame) return;
        gameCount = 0;
        SceneManager.LoadScene("GameOver");
    }

    public void GameWin() 
    {
        SceneManager.LoadScene("Victory");
    }
    
    public void PauseGame()           // Link to Game.PauseGameButton.OnClick
    {
        Debug.Log("Pause Game");
        _paused = !_paused;
        if (_paused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    
    public void ExitGame()           // Link to Game.ExitGameButton.OnClick
    {
        Debug.Log("Exit Game");
        SceneManager.LoadScene("Startup");
    }

    public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
    {
        Debug.Log("Quit Game");
        // exit unity game ?
        SceneManager.LoadScene("Startup"); // go back to startup for now
    }
}
