using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public IndvGameManager indvGameManager; 

    public float paddleSpeed = 15.0f;

    private float _horizontalInput;
    private const float PaddleXBound = 10.25f;
    private Vector3 _paddleInitialPosition;

    private void Awake()
    {
        _paddleInitialPosition = transform.localPosition;
    }

    public void ResetPaddle()
    {
        transform.localPosition = _paddleInitialPosition;
    }
    
    private void Move(float horizontalInput)
    {
        transform.Translate(Vector3.right * (Time.deltaTime * paddleSpeed * horizontalInput));

        // stay within bounds checks
        if (transform.localPosition.x < -PaddleXBound)
        {
            transform.localPosition = new Vector3(-PaddleXBound, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x > PaddleXBound)
        {
            transform.position = new Vector3(PaddleXBound, transform.localPosition.y, transform.localPosition.z);
        }
    }
    
    private void LateUpdate()
    {
        // horizontal input
        _horizontalInput = Input.GetAxis("Horizontal");
        Move(_horizontalInput);
    }
}
