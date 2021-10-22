using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // public vars
    public float difficultySpeed = 1.0f;
    public bool newGame;

    // private vars
    private Rigidbody2D ballBody;
    private Vector2 ballInitialVector;
    private Vector3 ballPosition;

    void Awake()
    {        
    }

    // Start is called before the first frame update
    void Start()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ballInitialVector = new Vector2(100.0f * difficultySpeed, -100.0f * difficultySpeed);
        ballPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // add force vector to rigidbody if just starting
        if (!newGame)
        {
            // add a force
            ballBody.AddForce(ballInitialVector);

            // set ball active
            newGame = !newGame;
        }
        Debug.Log($"\"{transform.position.x}\" , \"{transform.position.y}\"", this);
    }
}
