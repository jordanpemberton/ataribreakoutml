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
    public float brickHitReward = 20.0f;
    public float gameOverPenalty = -100.0f;
    public float victoryReward = 200.0f;
    
    private const float PaddleXBound = 10.25f;

    private Vector3 _paddleInitialPosition;

    private void Awake()
    {
        GameManager.Instance.humanPlayer = false;
        GameManager.Instance.score = 0;
        _paddleInitialPosition = transform.localPosition;
    }

    override public void OnEpisodeBegin()
    {
    	SetReward(0);
    	transform.localPosition = new Vector3(UnityEngine.Random.Range(-PaddleXBound/2f,PaddleXBound/2f), transform.localPosition.y, transform.localPosition.z);
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
    
    // Ball Hit reward is set here on collision event:
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BallController>(out BallController ball))
        {
            AddReward(ballHitReward);
        }
    }
}
