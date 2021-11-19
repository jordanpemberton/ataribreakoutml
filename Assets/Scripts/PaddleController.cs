using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float paddleSpeed = 10.0f;

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
        if (transform.position.x < -PaddleXBound)
        {
            transform.position = new Vector3(-PaddleXBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > PaddleXBound)
        {
            transform.position = new Vector3(PaddleXBound, transform.position.y, transform.position.z);
        }
    }
    
    private void LateUpdate()
    {
        // use input if human player
        if (GameManager.Instance.humanPlayer)
        {
            // horizontal input
            _horizontalInput = Input.GetAxis("Horizontal");
            Move(_horizontalInput);
        }
    }
}
