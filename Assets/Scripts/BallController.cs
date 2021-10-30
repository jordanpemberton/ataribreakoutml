using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 5.0f;

    // private vars
    private Rigidbody2D ballBody;
    private Vector3 ballInitialPosition;
    private Vector2 ballInitialForce;

    void ResetBall()
    {
        // set to initial position
        transform.position = ballInitialPosition;
        // add a force
        ballBody.velocity = Vector2.zero;
        ballBody.angularVelocity = 0f;
        ballBody.AddForce(ballInitialForce);
    }

    void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ballInitialPosition = new Vector3(-7f, 1f, 0f);
        // ballInitialForce    = new Vector2(100.0f * ballSpeed, -100.0f * ballSpeed);
        ballInitialForce    = new Vector2(200f * ballSpeed, 0f);

        ResetBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Floor")
        {
            // (game over)
            GameManager.instance.GameOver();

            // give some num tries first?
            // ResetBall();
        }
    }

    void LateUpdate()
    {
    }
}
