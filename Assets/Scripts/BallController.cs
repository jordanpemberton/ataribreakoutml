using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public EnvironmentManager envManager;
    
    public float ballSpeed = 15.0f;

    private Rigidbody2D _ballBody;
    private Vector3 _ballInitialPosition;
    private Vector2 _ballInitialForce;

    public void ResetBall()
    {
        // Reset to initial position
        transform.localPosition = _ballInitialPosition;
        // Add force
        _ballBody.velocity = Vector3.zero;
        _ballBody.AddForce(_ballInitialForce * ballSpeed);
    }

    private void Awake()
    {
        _ballBody = GetComponent<Rigidbody2D>();
        _ballInitialPosition = transform.localPosition;
        _ballInitialForce = new Vector2(100f, -100f);
        ResetBall();
    }

    // For some reason resetting ball with OnSceneLoaded makes it way faster???
    // private void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }
    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     ResetBall();
    // }
    // private void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // Prevent ball from bouncing in straight line forever:
    private void Update()
    {
        float minX = 1.0f;
        float minY = 1.0f;
        
        if (Mathf.Abs(_ballBody.velocity.x) < minX)
        {
            _ballBody.velocity = new Vector3(
                (_ballBody.velocity.x < 0.0f) ? -minX : minX,
                _ballBody.velocity.y );
        }
        
        if (Mathf.Abs(_ballBody.velocity.y) < minY)
        {
            _ballBody.velocity = new Vector3(
                _ballBody.velocity.x,
                (_ballBody.velocity.y < 0.0f) ? -minY : minY );
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.collider.gameObject.name;
        
        if (colliderName == "Floor")
        {
            // could maybe use distance from paddle to weight ML reward/penalties ?
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            else if (envManager != null)
            {
                envManager.GameOver();
            }
            else Debug.Log(("no manager found"));

            // give some num tries first?
        }
    }
}
