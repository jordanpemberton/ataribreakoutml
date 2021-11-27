using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; // one static singleton instance 

    public IndvGameManager humanGameManager;
    public IndvGameManager aiGameManager;
    
    public int numTriesPerGame = 1;
    public int gameCount = 0;
    private int _winner; // 0 = none, 1 = human, 2 = AI

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void NewGame()
    {
        gameCount = 0;
        
        SceneManager.LoadScene("Breakout");
        
        // trigger game manager's start game functions here...?
        if (humanGameManager == null) humanGameManager = GameObject.Find("HumanGameManager").GetComponent<IndvGameManager>();
        if (humanGameManager) humanGameManager.StartGame();

        if (aiGameManager == null) aiGameManager = GameObject.Find("AIGameManager").GetComponent<IndvGameManager>();
        if (aiGameManager) aiGameManager.StartGame();
    }
    
    public void GameOver(int winner)
    {
        if (winner == 1) Debug.Log("AI Lost");
        if (winner == 2) Debug.Log("Human Lost");

        // give some num tries first...
        gameCount++;
        if (gameCount == numTriesPerGame)
        {
            gameCount = 0;
            _winner = winner;
            SceneManager.LoadScene("GameOver");
            // Show who won, scores on GameOver screen text <--
        }
    }

    public void GameWin(int winner) 
    {
        if (winner == 1) Debug.Log("Human Wins!");
        if (winner == 2) Debug.Log("AI Wins!");

        _winner = winner;
        SceneManager.LoadScene("Victory");
        // Show who won, scores on Victory screen text <--
    }

    public void ExitGame()           // Link to Game.ExitGameButton.OnClick
    {
        SceneManager.LoadScene("Startup");
    }

    public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
    {
        // exit unity game ?
        SceneManager.LoadScene("Startup"); // go back to startup for now
    }
}
