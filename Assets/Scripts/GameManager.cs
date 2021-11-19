using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // private singleton instance, access from anywhere
    public static GameManager Instance;

    public bool humanPlayer;

    public GameObject scoreTextObject;
    public GameObject levelATextObject;
    public GameObject ball;
    public GameObject bricks;
    public GameObject paddleAI;

    private BallController _ballController;
    private BricksController _bricksController;
    private PaddleAgent _agent;
    
    public int score = 0;

    private void LinkGameObjects()
    {
        if (bricks == null) bricks = GameObject.Find("Bricks");
        if (bricks == null) Debug.Log("GameObject 'Bricks' not found.");
        if (bricks != null) _bricksController = bricks.GetComponent<BricksController>();

        if (ball == null) ball = GameObject.Find("Ball");
        if (ball == null) Debug.Log("GameObject 'Ball' not found.");
        if (ball != null) _ballController = ball.GetComponent<BallController>();

        if (scoreTextObject == null) scoreTextObject = GameObject.Find("ScoreText");
        if (scoreTextObject == null) Debug.Log("GameObject 'ScoreText' not found.");
        
        if (levelATextObject == null) levelATextObject = GameObject.Find("LevelAText");
        if (levelATextObject == null) Debug.Log("GameObject 'LevelAText' not found.");
            
        if (paddleAI == null) paddleAI = GameObject.Find("PaddleAI");
        if (paddleAI == null)
        {
            humanPlayer = true;
        }
        else
        {
            humanPlayer = false;
            _agent = paddleAI.GetComponent<PaddleAgent>();
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Breakout")
        {
            LinkGameObjects();
            score = 0;
            if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
            if (levelATextObject != null && _agent != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        Debug.Log("Start New Game");
        
        if (humanPlayer)
        {
            SceneManager.LoadScene("Breakout");
            LinkGameObjects();
        }
        else
        {
            LinkGameObjects();
            // Reset PaddleAI
            if (_agent != null) _agent.ResetPaddle();
            // Reset bricks
            if (_ballController != null) _bricksController.ResetBricks();
            // Reset ball
            if (_ballController != null) _ballController.ResetBall();
            
            if (levelATextObject != null && _agent != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
        }
        
        // Reset score
        score = 0;
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
    }

    public void AddScore(int points)
    {
        // Update score
        score += points;
        // Debug.Log($"Score = {score}");
        
        // Update score text
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
        
        // If AI, reward 
        if (!humanPlayer && _agent != null)
        {
            _agent.AddReward(_agent.brickHitReward);
            if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        // If human player, show GameOver screen
        if (humanPlayer)
        {
            SceneManager.LoadScene("GameOver");
        }
        // If AI, penalize and start new game
        else
        {
            if (_agent != null)
            {
                _agent.AddReward(_agent.gameOverPenalty);
                if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
                _agent.EndEpisode();
            }
            StartGame();
        }
    }

    public void GameWin() 
    {
        Debug.Log("You Win!");

        // If human player show Victory screen
        if (humanPlayer)
        {
            SceneManager.LoadScene("Victory");
        }
        // If AI, reward and start new game 
        else
        {
            if (_agent != null)
            {
                _agent.AddReward(_agent.victoryReward);
                if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
                // end episode?
            }
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
