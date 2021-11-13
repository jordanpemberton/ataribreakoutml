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
    public GameObject paddleAI;
    private PaddleAgent _agent;
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

    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        SceneManager.LoadScene("Breakout");
        
        if (humanPlayer)
        {
            Debug.Log("Start New Game");
        }
        else
        {
            if (paddleAI == null)
            {
                paddleAI = GameObject.Find("PaddleAI");
                if (paddleAI != null) _agent = paddleAI.GetComponent<PaddleAgent>();
            }
        }
        
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
        // Update score
        score += points;

        // Update score text
        if (scoreTextObject == null)
        {
            scoreTextObject = GameObject.Find("ScoreText");
            if (scoreTextObject == null)
            {
                if (humanPlayer) Debug.Log("GameObject 'ScoreText' not found.");
                return;
            }
        }
        scoreTextObject.GetComponent<Text>().text = score.ToString("D3");

        // If AI, reward 
        if (!humanPlayer && _agent != null)
        {
            _agent.SetReward(_agent.brickHitReward);
        }
    }

    public void GameOver()
    {
        // If human player, show GameOver screen
        if (humanPlayer)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
        // If AI, penalize and start new game
        else
        {
            if (_agent != null) _agent.SetReward(_agent.gameOverPenalty);
            StartGame();
        }
    }

    public void GameWin() 
    {
        // If human player show Victory screen
        if (humanPlayer)
        {
            Debug.Log("You Win!");
            SceneManager.LoadScene("Victory");
        }
        // If AI, reward and start new game 
        else
        {
            if (_agent != null) _agent.SetReward(_agent.victoryReward);
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
