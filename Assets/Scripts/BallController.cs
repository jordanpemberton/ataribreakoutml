using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 5.0f;

    // public GameObject newBall;

    // private vars
    private Rigidbody2D ballBody;
    private Vector3 ballInitialPosition;
    private Vector2 ballInitialVector;

    void StartBall()
    {
        // set to initial position
        transform.position = ballInitialPosition;
        // add a force
        ballBody.AddForce(ballInitialVector);
    }

    void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ballInitialPosition = new Vector3(-7f, 1f, 0f);
        ballInitialVector   = new Vector2(100.0f * ballSpeed, -100.0f * ballSpeed);
        StartBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Floor")
        {
            // (game over)
            Debug.Log("GAME OVER");

            // for now
            StartBall();
        }
    }

    void LateUpdate()
    {
    }
}
