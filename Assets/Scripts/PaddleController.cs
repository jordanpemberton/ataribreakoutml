using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    GlobalData d;

    public float paddleSpeed = 10.0f;

    private float horizontalInput;
    private float paddle_x_bound;
    private Vector3 paddle_scale;


    void Awake()
    {
        d = GlobalData.GetInstance();

        // set paddle x-bounds from layout data
        paddle_x_bound = d.SCENE_W / 2f - d.PADDING - d.WALL_W - paddle_scale.x / 2f;
    }

    void LateUpdate()
    {
        // use input if human player
        if (GameManager.instance.humanPlayer)
        {
            // horizontal input
            horizontalInput = Input.GetAxis("Horizontal");
            // new_x += Time.deltaTime * paddleSpeed * horizontalInput;
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
