using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // public vars

    // private vars
    private Rigidbody2D ballBody;
    private Vector2 ballInitialVector;
    private Vector3 ballPosition;

    void Awake()
    {        
    }

    // Start is called before the first frame update
    void Start()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ballInitialVector = new Vector2(1.0f, -1.0f);
        ballPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // add force vector to rigidbody
        ballBody.AddForce(ballInitialVector);
        Debug.Log($"\"{transform.position.x}\" , \"{transform.position.y}\"", this);
    }
}
