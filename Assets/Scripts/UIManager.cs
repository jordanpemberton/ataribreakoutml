using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // It might make more sense for these actions to be handled by a GameManager script,
    // but I added them here to create the UI.

    public void StartGame()         // Link to Startup.StartGameButton.OnClick
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("Game");
    }

    public void NewGame()           // Link to GameOver.NewGameButton.OnClick
    {
        Debug.Log("New Game");
        SceneManager.LoadScene("Startup");
    }

    public void GameOver()          //
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }

    public void ExitGame()           // Link to Game.ExitGameButton.OnClick
    {
        Debug.Log("Quit Game");
        SceneManager.LoadScene("Startup");
    }

    public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
    {
        Debug.Log("Quit Game");
        // exit game ?
    }
}
