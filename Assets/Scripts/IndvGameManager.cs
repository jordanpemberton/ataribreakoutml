using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IndvGameManager : MonoBehaviour
{
    public int numTriesPerGame = 1;
    public int gameCount = 0;
    public int score = 0;
    public bool humanPlayer = false;

    public GameObject scoreTextObject;
    public GameObject levelATextObject;
    public GameObject levelBTextObject;
    public GameObject ball;
    public GameObject bricks;
    public GameObject paddleHuman;
    public GameObject paddleAI; 
    
    private BallController _ballController;
    private BricksController _bricksController;
    private PaddleController _paddleController; // if humanPlayer
    private PaddleAgent _paddleAgent;   // if not humanPlayer

    private void CheckForGameObjects()
    {
        if (bricks == null) Debug.Log("GameObject 'Bricks' not found.");
        if (bricks != null) _bricksController = bricks.GetComponent<BricksController>();

        if (ball == null) Debug.Log("GameObject 'Ball' not found.");
        if (ball != null) _ballController = ball.GetComponent<BallController>();

        if (scoreTextObject == null) Debug.Log("GameObject 'ScoreText' not found.");
        if (levelATextObject == null) Debug.Log("GameObject 'LevelAText' not found.");
        if (levelBTextObject == null) Debug.Log("GameObject 'LevelBText' not found.");

        if (humanPlayer)
        {
            if (paddleHuman == null) Debug.Log("GameObject 'PaddleHuman' not found.");
            if (paddleHuman != null) _paddleController = paddleHuman.GetComponent<PaddleController>();
        }
        else
        {
            if (paddleAI == null) Debug.Log("GameObject 'PaddleAI' not found.");
            if (paddleAI != null) _paddleAgent = paddleAI.GetComponent<PaddleAgent>();
        } 
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Breakout") return;
        
        CheckForGameObjects();
        
        score = 0;
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
        // set level or game count or whatever other text here
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        // Check for objects, link controllers
        CheckForGameObjects();
        
        // Reset game objects
        if (_bricksController != null) _bricksController.ResetBricks();   
        if (_ballController != null) _ballController.ResetBall();       
        if (_paddleController != null) _paddleController.ResetPaddle(); 
        if (_paddleAgent != null) _paddleAgent.ResetPaddle();
        
        // Reset score
        score = 0;                                                      
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
        // reset any other text here
    }

    public void AddScore(int points)
    {
        // Update score
        score += points;
        if (scoreTextObject != null) scoreTextObject.GetComponent<Text>().text = score.ToString("D3");
    }

    public void GameOver()
    {
        // give some num tries first...
        gameCount++;
        if (levelBTextObject != null) levelBTextObject.GetComponent<Text>().text = gameCount.ToString("D2");

        if (gameCount == numTriesPerGame)
        {
            gameCount = 0;
            GameManager.Instance.GameOver(humanPlayer ? 2 : 1);
        }
    }

    public void GameWin() 
    {
        GameManager.Instance.GameWin(humanPlayer ? 1 : 2);
    }

    // public void ExitGame()           // Link to Game.ExitGameButton.OnClick
    // {
    //     SceneManager.LoadScene("Startup");
    // }

//     public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
//     {
//         // exit unity game ?
//         SceneManager.LoadScene("Startup"); // go back to startup for now
//     }
}
