using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // public vars
    public float difficultySpeed = 1.0f;
    public bool newBallSpawn;
    
    public GameObject newBall;

    // private vars
    private Camera mainCamera;
    private Rigidbody2D ballBody;
    private Vector2 ballInitialVector;
    private Vector3 ballInitialPosition;
    private Vector3 ballPosition;
    private Vector3 screenBounds;
    private float yBound;

    // Start is called before the first frame update
    void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ballInitialVector = new Vector2(100.0f * difficultySpeed, -100.0f * difficultySpeed);
        ballInitialPosition = new Vector3(-8.0f, 4.0f, 0.0f);
        mainCamera = Camera.main;
        screenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        yBound = screenBounds.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // add force vector to rigidbody if just starting
        if (!newBallSpawn)
        {
            // add a force
            ballBody.AddForce(ballInitialVector);

            // set ball inactive
            newBallSpawn = !newBallSpawn;
        }

        // check if game over
        ballPosition = transform.position;
        if (ballPosition.y < -yBound - 1.0f)
        {
            // spawn new ball
            newBallSpawn = !newBallSpawn;
            GameObject nextBall = Instantiate(newBall, ballInitialPosition, transform.rotation);
            nextBall.name = nextBall.name.Replace("(Clone)", "");
            Destroy(gameObject);
        }
    }
}
