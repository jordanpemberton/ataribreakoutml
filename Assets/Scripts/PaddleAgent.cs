using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PaddleAgent : Agent
{
    public EnvironmentManager envManager; // for training

    [SerializeField] private Transform ballTransform; // link to ball

    public float paddleSpeed = 15.0f;
    
    // ML agent rewards:
    public float ballHitReward = 10.0f;  
    public float brickHitReward = 10.0f;
    public float ballPaddleDistancePenalty = -2.0f; // per unit from center of paddle, 0..13 
    public float gameOverPenalty = -10.0f;
    public float victoryReward = 10.0f; 
    
    private const float PaddleXBound = 10.25f;
    private Vector3 _paddleInitialPosition;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.humanPlayer = false;
            GameManager.Instance.score = 0;
        }
        else if (envManager != null)
        {
            envManager.humanPlayer = false;
            envManager.score = 0;
        }
        _paddleInitialPosition = transform.localPosition;
    }
    
    public void ResetPaddle()
    {
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-PaddleXBound / 2.0f, PaddleXBound / 2.0f), 
            transform.localPosition.y, transform.localPosition.z);
    }

    private void Move(float horizontalInput)
    {
        transform.Translate(Vector3.right * Time.deltaTime * paddleSpeed * horizontalInput);

        // stay within bounds checks
        if (transform.localPosition.x < -PaddleXBound)
        {
            transform.localPosition = new Vector3(-PaddleXBound, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x > PaddleXBound)
        {
            transform.localPosition = new Vector3(PaddleXBound, transform.localPosition.y, transform.localPosition.z);
        }
    }
    
    // For human player use (when developing)
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }

    public override void OnEpisodeBegin()
    {
        ResetPaddle();
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        // paddle position
        sensor.AddObservation(transform.localPosition);
        
        // ball position
        sensor.AddObservation(ballTransform != null ? ballTransform.localPosition : Vector3.zero);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0]; // only need X-axis movement /input
        Move(moveX);
    }
    
    // GameOver, Victory, and Score rewards are all set in GameManager on corresponding events
    
    // Called when GameOver to set distance missed by penalty:
    public void MissDistanceReward()
    {
        if (ballTransform != null)
        {
            float xDist = Math.Abs(transform.localPosition.x - ballTransform.localPosition.x);
            float reward = (ballPaddleDistancePenalty * xDist);
            AddReward(reward);
            UpdateRewardText();
        }
    }
    
    // Ball Hit reward is set here on collision event:
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BallController>(out BallController ball))
        {
            AddReward(ballHitReward);
            UpdateRewardText();
        }
    }

    private void UpdateRewardText()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.levelATextObject != null)
            {
                GameManager.Instance.levelATextObject.GetComponent<Text>().text = GetCumulativeReward().ToString("F6");
            }
        }
        else if (envManager != null)
        {
            if (envManager.levelATextObject != null)
            {
                envManager.levelATextObject.GetComponent<Text>().text = GetCumulativeReward().ToString("F6");
            }
        }
    }
}
