using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    GlobalData d;

    public float ballSpeed = 5.0f;

    // public GameObject newBall;

    // private vars
    private Rigidbody2D ballBody;
    private Vector2 ballInitialVector;
    private Vector3 ballInitialPosition;
    private Vector3 ballPosition;

    void StartBall()
    {
        // set to initial position
        transform.position = ballInitialPosition;
        // add a force
        ballBody.AddForce(ballInitialVector);
    }

    void Awake()
    {
        Debug.Log(GameManager.instance.screenBounds.y);
        d = GlobalData.GetInstance();

        ballBody = GetComponent<Rigidbody2D>();
        ballInitialVector = new Vector2(100.0f * ballSpeed, -100.0f * ballSpeed);
        ballInitialPosition = new Vector3(-8f, -2f, 0.0f);
        StartBall();
    }

    void LateUpdate()
    {
        // check if game over
        if (transform.position.y < (GameManager.instance.screenBounds.y - 1f))
        {
            // if out of tries, display game over screen:
            ;

            // else count down tries, reset game
            // StartBall();
        }
    }
}
