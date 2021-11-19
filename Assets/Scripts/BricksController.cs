using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;

public class BricksController : MonoBehaviour
{
    public GameObject brickObject;

    private List<GameObject> _activeBricks = new List<GameObject>();

    private const float X0 = -12f;
    private const float Y0 = 7f;
    private const float BrickH = 0.5f;
    private const float BrickW = 2f;

    private readonly Color[] _brickColors = new Color[] {
        Color.grey,
        Color.white,
        Color.magenta,
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue
    };

    public void RemoveBrick(BrickController brick)
    {
        _activeBricks.Remove(brick.gameObject);

        // +score
        GameManager.Instance.AddScore(1);

        if (_activeBricks.Count == 0) GameManager.Instance.GameWin();
    }

    private void CreateBricks()
    {
        // instantiate child brick game objects
        for (int i=0; i<13; i++)
        {
            for (int j=0; j<7; j++)
            {
                GameObject brick = Instantiate(brickObject, transform, true);

                float x = X0 + i * BrickW;
                float y = Y0 - j * BrickH;

                brick.transform.localPosition = new Vector2(x, y);
                brick.transform.localScale = new Vector2(BrickW-0.1f, BrickH-0.1f);

                SpriteRenderer rend = brick.GetComponent<SpriteRenderer>();
                rend.material.color = _brickColors[j];

                _activeBricks.Add(brick);
            }
        }
    }

    public void ResetBricks()
    {
        foreach (GameObject brick in _activeBricks)
        {
            Destroy(brick);
        }
        _activeBricks = new List<GameObject>();

        CreateBricks();
    }

    private void Awake()
    {
        // link prefab if not already linked
        if (brickObject == null)
        {
            brickObject = Resources.Load("Prefabs/Brick", typeof(GameObject)) as GameObject;
            if (brickObject == null)  Debug.Log("Prefab 'Brick' not found.");
        }
    }
    
    private void Start()
    {
        CreateBricks();
    }
}
