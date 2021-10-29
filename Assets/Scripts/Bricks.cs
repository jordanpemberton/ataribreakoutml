using UnityEngine;
using System.Collections.Generic;

public class Bricks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BrickObject;

    Color[] colors = new Color[] { Color.grey, Color.white, Color.magenta, Color.red, Color.yellow, Color.green, Color.blue};

    public List<GameObject> allBricks = new List<GameObject>();

    void Start()
    {
        RestartLevel();
    }

    public void RestartLevel()
    {
        foreach (GameObject b in allBricks)
        {
            Destroy(b);
        }

        for (int y = 0; y < 7; y++)
        {
            for (int x = 0; x < 13; x++)
            {
                GameObject b = Instantiate(BrickObject);

                b.transform.position = new Vector2(x * (float)5 - 30, 14 - y * 1.5f);

                SpriteRenderer rend = b.GetComponent<SpriteRenderer>();
                rend.material.color = colors[y];

                allBricks.Add(b);
            }

        }
    }

    public void RemoveBrick(Brick brick)
    {
        allBricks.Remove(brick.gameObject);
        if (allBricks.Count == 0)
        {
            RestartLevel();
            //Display Jordan's victory screen;
        }
    }
}
