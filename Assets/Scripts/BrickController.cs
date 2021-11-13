using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<BallController>(out BallController ball))
        {
            Destroy(gameObject);
            transform.parent.GetComponent<BricksController>().RemoveBrick(this);
        }
    }
}
