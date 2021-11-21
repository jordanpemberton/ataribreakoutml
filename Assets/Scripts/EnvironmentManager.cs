using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnvironmentManager : MonoBehaviour
{
    public bool humanPlayer = false;

    public GameObject scoreTextObject;
    public GameObject levelATextObject;
    public GameObject ball;
    public GameObject bricks;
    public GameObject paddleAI;

    private BallController _ballController;
    private BricksController _bricksController;
    private PaddleAgent _agent;
    
    public int score = 0;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Training")
        {
            CheckForGameObjects();
            score = 0;
            if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
            if (levelATextObject != null && _agent != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void CheckForGameObjects()
    {
        if (bricks == null) Debug.Log("GameObject 'Bricks' missing.");
        if (bricks != null) _bricksController = bricks.GetComponent<BricksController>();

        if (ball == null) Debug.Log("GameObject 'Ball' missing.");
        if (ball != null) _ballController = ball.GetComponent<BallController>();

        if (scoreTextObject == null) Debug.Log("GameObject 'ScoreText' missing.");
        
        if (levelATextObject == null) Debug.Log("GameObject 'LevelAText' missing.");
            
        if (paddleAI == null) Debug.Log("GameObject 'PaddleAI' missing.");
        if (paddleAI != null) _agent = paddleAI.GetComponent<PaddleAgent>();
    }

    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        CheckForGameObjects();
        // Reset PaddleAI
        if (_agent != null) _agent.ResetPaddle();
        // Reset bricks
        if (_ballController != null) _bricksController.ResetBricks();
        // Reset ball
        if (_ballController != null) _ballController.ResetBall();
        // Reset rewards text
        if (levelATextObject != null && _agent != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
        // Reset score
        score = 0;
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
    }

    public void AddScore(int points)
    {
        // Update score
        score += points;
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
        
        // Reward AI 
        _agent.AddReward(_agent.brickHitReward);
        if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
    }

    public void GameOver()
    {
        _agent.AddReward(_agent.gameOverPenalty);
        if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
        _agent.EndEpisode();
        StartGame();
    }

    public void GameWin() 
    {
        _agent.AddReward(_agent.victoryReward);
        if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _agent.GetCumulativeReward().ToString("F6");
        // end episode?
        StartGame();
    }
}
