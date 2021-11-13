using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 15.0f;

    private Rigidbody2D ballBody;
    private Vector3 ballInitialPosition;
    private Vector2 ballInitialForce;

    private void ResetBall()
    {
        // Reset to initial position
        transform.position = ballInitialPosition;
        // Add force
        ballBody.AddForce(ballInitialForce * ballSpeed);
    }

    void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ballInitialPosition = new Vector3(-7f, 1f, 0f);
        ballInitialForce = new Vector2(100f, -100f);
        ResetBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collider = collision.collider.gameObject.name;
        
        if (collider == "Floor")
        {
            // could maybe use distance from paddle to weight ML reward/penalties ?
            GameManager.Instance.GameOver();

            // give some num tries first?
        }
    }
}
