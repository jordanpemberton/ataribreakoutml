using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    public GameObject winnerText;
    public GameObject humanScoreText;
    public GameObject aiScoreText;

    private void ShowFinalScores()
    {
        if (humanScoreText == null) Debug.Log("'HumanScoreText' not found.");
        else humanScoreText.GetComponent<Text>().text = GameManager.Instance.humanScore.ToString("D3");

        if (aiScoreText == null) Debug.Log("'AIScoreText' not found.");
        else aiScoreText.GetComponent<Text>().text = GameManager.Instance.aiScore.ToString("D3");
    }

    private void ShowWinner()
    {
        if (GameManager.Instance.humanScore > GameManager.Instance.aiScore)
        {
            winnerText.GetComponent<Text>().text = "HUMAN WINS";
        }
        else if (GameManager.Instance.humanScore < GameManager.Instance.aiScore)
        {
            winnerText.GetComponent<Text>().text = "AI WINS";
        }
        else
        {
            winnerText.GetComponent<Text>().text = "TIE";
        }
    }
    
    private void Start() 
    {
        // Show winner 
        if (winnerText == null)
        {
            Debug.Log("'WinnerText' not found.");
            return;
        }
        
        ShowWinner();
        ShowFinalScores();
    }
}
