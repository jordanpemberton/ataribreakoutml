using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // private singleton instance, access from anywhere
    public static GameManager Instance;

    public bool humanPlayer = true;
    public GameObject scoreTextObject;
    public int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // this currently only gets called when a new game is started from the startup screen... how to always call?
    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        if (humanPlayer) Debug.Log("Start New Game");

        SceneManager.LoadScene("Breakout");

        // link prefab if not already linked
        if (scoreTextObject == null)
        {
            scoreTextObject = GameObject.Find("ScoreText");
            if (scoreTextObject == null)
            {
                if (humanPlayer) Debug.Log("GameObject 'ScoreText' not found.");
            }
        }
        score = 0;
    }

    public void AddScore(int points)
    {
        score += points;

        if (scoreTextObject == null)
        {
            scoreTextObject = GameObject.Find("ScoreText");
            if (scoreTextObject == null)
            {
                if (humanPlayer) Debug.Log("GameObject 'ScoreText' not found.");
            }
            // score = 0; // ?
            return;
        }

        scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
    }

    public void GameOver()
    {
        if (humanPlayer)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            StartGame();
        }
    }

    public void GameWin() 
    {
        if (humanPlayer)
        {
            Debug.Log("You Win!");
            SceneManager.LoadScene("Victory");
        }
        else
        {
            StartGame();
        }
    }

    public void ExitGame()           // Link to Game.ExitGameButton.OnClick
    {
        if (humanPlayer) Debug.Log("Exit Game");
        SceneManager.LoadScene("Startup");
    }

    public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
    {
        if (humanPlayer) Debug.Log("Exit Game");
        // exit unity game ?
        SceneManager.LoadScene("Startup"); // go back to startup for now
    }
}
