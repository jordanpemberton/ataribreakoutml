using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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

    public void QuitGame()           // Link to GameOver.QuitGameButton.OnClick
    {
        Debug.Log("Quit Game");
        // exit game ?
    }
}
