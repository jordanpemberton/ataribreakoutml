using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // public vars
    public bool humanPlayer = true;
    public float speed = 5.0f;

    // private vars
    private Camera mainCamera;
    private float horizontalInput;
    private Vector3 screenBounds;
    private float xBound;
    private Vector3 paddleScale;
    private float paddleLength;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        xBound = screenBounds.x;
        paddleScale = transform.localScale;
        paddleLength = paddleScale.x / 2.0f;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // move object if human player
        if (humanPlayer)
        {
            // border checks
            if (transform.position.x < -xBound + paddleLength)
            {
                transform.position = new Vector3(-xBound + paddleLength, transform.position.y, transform.position.z);
            }
            if (transform.position.x > xBound - paddleLength)
            {
                transform.position = new Vector3(xBound - paddleLength, transform.position.y, transform.position.z);
            }
            
            // horizontal movement
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }
    }
}
