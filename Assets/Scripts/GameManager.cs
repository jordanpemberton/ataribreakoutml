using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // private singleton instance, access from anywhere
    public static GameManager instance = null;

    public bool  humanPlayer = true;

    public GameObject ScoreTextObject;
    public int score = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // void Start()
    // {
    //     // link prefab if not already linked
    //     if (ScoreTextObject == null)
    //     {
    //         ScoreTextObject = GameObject.Find("ScoreText");
    //         if (ScoreTextObject == null)
    //         {
    //             Debug.Log("GameObject 'ScoreText' not found.");
    //             return;
    //         }
    //     }

    //     score = 0;
    // }

    public void StartGame()   // Link to Startup.StartGameButton.OnClick, GameOver.NewGameButton.OnClick
    {
        Debug.Log("Start New Game");

        SceneManager.LoadScene("Breakout");

        // link prefab if not already linked
        if (ScoreTextObject == null)
        {
            ScoreTextObject = GameObject.Find("ScoreText");
            if (ScoreTextObject == null)
            {
                Debug.Log("GameObject 'ScoreText' not found.");
                return;
            }
        }

        score = 0;
    }

    public void AddScore(int points)
    {
        if (ScoreTextObject == null)
        {
            ScoreTextObject = GameObject.Find("ScoreText");
            if (ScoreTextObject == null)
            {
                Debug.Log("GameObject 'ScoreText' not found.");
                return;
            }
            score = 0;
        }

        Debug.Log($"+{points} point(s)");
        score += points;
        ScoreTextObject.GetComponent<Text>().text = score.ToString("D3");
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
        // exit unity game ?
        SceneManager.LoadScene("Startup"); // go back to startup for now
    }
}
