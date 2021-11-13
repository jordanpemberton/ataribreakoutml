using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine.SocialPlatforms.Impl;

public class PaddleAgent : Agent
{
    [SerializeField] private Transform ballTransform; // link to ball

    public float paddleSpeed = 15.0f;
    
    // ML agent rewards:
    public float ballHitReward = 5.0f;  
    public float brickHitReward = 5.0f;  
    public float gameOverPenalty = -20.0f;
    public float victoryReward = 20.0f; // maybe unnecessary?
    
    private const float PaddleXBound = 10.25f;

    private int _score = 0;
    private bool _gameOver = false;
    private bool _victory = false;

    private void Awake()
    {
        GameManager.Instance.humanPlayer = false;
        GameManager.Instance.score = 0;
    }
    private void Move(float horizontalInput)
    {
        transform.Translate(Vector3.right * Time.deltaTime * paddleSpeed * horizontalInput);

        // stay within bounds checks
        if (transform.position.x < -PaddleXBound)
        {
            transform.position = new Vector3(-PaddleXBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > PaddleXBound)
        {
            transform.position = new Vector3(PaddleXBound, transform.position.y, transform.position.z);
        }
    }
    
    // For human player use (developing)
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        // paddle position
        sensor.AddObservation(transform.localPosition);
        
        // ball position
        if (ballTransform != null)
        {
            sensor.AddObservation(ballTransform.localPosition);
        }
        else
        {
            sensor.AddObservation(Vector3.zero);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0]; // only need X-axis movement /input
        Move(moveX);
        
        // Rewards/penalties
        // Check if game over or won
        if (_gameOver) SetReward(gameOverPenalty);
        if (_victory) SetReward(victoryReward);

        // check/update score
        if (GameManager.Instance.score == _score) return;
        if (GameManager.Instance.score > _score)
            SetReward(brickHitReward);
        _score = GameManager.Instance.score;
    }
    
    // Rewards
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BallController>(out BallController ball))
        {
            SetReward(ballHitReward);
        }
    }
}
