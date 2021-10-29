using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy(gameObject);
        transform.GetComponent<SpriteRenderer>().enabled = false;  // hide brick
        // FindObjectOfType<Bricks>().RemoveBrick(this);
        transform.parent.GetComponent<BricksController>().RemoveBrick(this);
    }
}
