using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject humanScoreText;
    public GameObject aiScoreText;

    private void ShowFinalScores()
    {
        if (humanScoreText == null) Debug.Log("'HumanScoreText' not found.");
        else humanScoreText.GetComponent<Text>().text = GameManager.Instance.humanScore.ToString("D3");

        if (aiScoreText == null) Debug.Log("'AIScoreText' not found.");
        else aiScoreText.GetComponent<Text>().text = GameManager.Instance.aiScore.ToString("D3");
    }

    private void Start()
    {
        ShowFinalScores();
    }
}
