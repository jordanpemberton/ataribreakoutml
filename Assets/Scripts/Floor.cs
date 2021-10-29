using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<Bricks>().RestartLevel();
        // Display Jordan's defeat screen
    }
}
