using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.collider.gameObject.name;
        
        if (colliderName == "Floor")
        {
            // could maybe use distance from paddle to weight ML reward/penalties ?
            GameManager.Instance.GameOver();

            // give some num tries first?
        }
    }
}
