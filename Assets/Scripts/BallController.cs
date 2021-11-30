using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public EnvironmentManager envManager;
    public IndvGameManager indvGameManager;
    
    public float ballSpeed;

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
        float xMin = 1.0f;
        float yMin = 1.0f;
        float xAdjust = 10.0f;
        float yAdjust = 10.0f;
        
        if ((-xMin < _ballBody.velocity.x) && (_ballBody.velocity.x < xMin)) 
        {
            _ballBody.velocity = new Vector2(
                (_ballBody.velocity.x < 0.0f) ? -xAdjust : xAdjust,
                _ballBody.velocity.y );
        }
        
        if ((-yMin < _ballBody.velocity.y) && (_ballBody.velocity.y < yMin)) 
        {
            _ballBody.velocity = new Vector2(
                _ballBody.velocity.x,
                (_ballBody.velocity.y < 0.0f) ? -yAdjust : yAdjust );
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.collider.gameObject.name;

        if (colliderName != "Floor") return;
        
        if (indvGameManager != null) indvGameManager.GameOver();
        else if (envManager != null) envManager.GameOver();
        else Debug.Log(("no manager found"));
    }
}
