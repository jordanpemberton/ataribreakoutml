using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float paddleSpeed = 10.0f;

    private float horizontalInput;
    private float paddle_x_bound;
    private Vector3 paddle_scale;


    void Awake()
    {
        // set paddle x-bounds to match left/right walls inner faces
        paddle_x_bound = 10.25f; 
    }

    void LateUpdate()
    {
        // use input if human player
        if (GameManager.instance.humanPlayer)
        {
            // horizontal input
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * paddleSpeed * horizontalInput);
        }
        // if AI player
        else
        {
            ;
        }

        // stay within bounds checks
        if (transform.position.x < -paddle_x_bound)
        {
            transform.position = new Vector3(-paddle_x_bound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > paddle_x_bound)
        {
            transform.position = new Vector3(paddle_x_bound, transform.position.y, transform.position.z);
        }
    }
}
