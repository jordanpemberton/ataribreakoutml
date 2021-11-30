using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnvironmentManager : MonoBehaviour  // Manager for AI Training Environments 
{
    public bool humanPlayer = false;
    public int numGamesPerEpisodes = 1; // does this actually affect training? might be useful for human player tho...
    public int gameCount = 0;
    public int score = 0;

    public GameObject scoreTextObject;
    public GameObject levelATextObject;
    public GameObject levelBTextObject;
    public GameObject ball;
    public GameObject bricks;
    public GameObject paddleAI;

    private BallController _ballController;
    private BricksController _bricksController;
    private PaddleAgent _paddleAgent;
    
    private void CheckForGameObjects()
    {
        if (bricks == null) Debug.Log("GameObject 'Bricks' missing.");
        if (bricks != null) _bricksController = bricks.GetComponent<BricksController>();

        if (ball == null) Debug.Log("GameObject 'Ball' missing.");
        if (ball != null) _ballController = ball.GetComponent<BallController>();

        if (scoreTextObject == null) scoreTextObject = GameObject.Find("ScoreText");
        if (scoreTextObject == null) Debug.Log("GameObject 'ScoreText' not found.");
        
        if (levelATextObject == null) levelATextObject = GameObject.Find("LevelAText");
        if (levelATextObject == null) Debug.Log("GameObject 'LevelAText' not found.");
        
        if (levelBTextObject == null) levelBTextObject = GameObject.Find("LevelBText");
        if (levelBTextObject == null) Debug.Log("GameObject 'LevelBText' not found.");
            
        if (paddleAI == null) Debug.Log("GameObject 'PaddleAI' missing.");
        if (paddleAI != null) _paddleAgent = paddleAI.GetComponent<PaddleAgent>();
    }
    
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
            if (levelATextObject != null && _paddleAgent != null) levelATextObject.GetComponent<Text>().text = _paddleAgent.GetCumulativeReward().ToString("F6");
            if (levelBTextObject != null && _paddleAgent != null) levelBTextObject.GetComponent<Text>().text = gameCount.ToString("D2");
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        CheckForGameObjects();
        if (_paddleAgent != null) _paddleAgent.ResetPaddle();
        if (_bricksController != null) _bricksController.ResetBricks();
        if (_ballController != null) _ballController.ResetBall();
        if (levelATextObject != null && _paddleAgent != null) levelATextObject.GetComponent<Text>().text = _paddleAgent.GetCumulativeReward().ToString("F6");
        score = 0;
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
    }

    public void AddScore(int points)
    {
        // Update score
        score += points;
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
        
        // Reward AI 
        _paddleAgent.AddReward(_paddleAgent.brickHitReward);
        if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _paddleAgent.GetCumulativeReward().ToString("F6");
    }

    public void GameOver()
    {
        // Penalize AI
        _paddleAgent.AddReward(_paddleAgent.gameOverPenalty);
        _paddleAgent.MissDistanceReward();
        
        if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _paddleAgent.GetCumulativeReward().ToString("F6");
        
        gameCount++;
        if (levelBTextObject != null && _paddleAgent != null) levelBTextObject.GetComponent<Text>().text = gameCount.ToString("D2");

        if (gameCount == numGamesPerEpisodes)
        {
            _paddleAgent.EndEpisode();
            _paddleAgent.SetReward(0);
            gameCount = 0;
            if (levelBTextObject != null && _paddleAgent != null) levelBTextObject.GetComponent<Text>().text = gameCount.ToString("D2");
        }
        
        StartGame();
    }

    public void GameWin() 
    {
        _paddleAgent.AddReward(_paddleAgent.victoryReward);
        if (levelATextObject != null) levelATextObject.GetComponent<Text>().text = _paddleAgent.GetCumulativeReward().ToString("F6");
        
        gameCount++;
        if (levelBTextObject != null && _paddleAgent != null) levelBTextObject.GetComponent<Text>().text = gameCount.ToString("D2");

        if (gameCount == numGamesPerEpisodes)
        {
            _paddleAgent.EndEpisode();
            _paddleAgent.SetReward(0);
            gameCount = 0;
            if (levelBTextObject != null && _paddleAgent != null) levelBTextObject.GetComponent<Text>().text = gameCount.ToString("D2");
        }
        
        StartGame();
    }
}
