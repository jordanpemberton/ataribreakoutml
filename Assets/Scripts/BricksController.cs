using UnityEngine;
using System.Collections.Generic;

public class BricksController : MonoBehaviour
{
    public GameObject BrickObject;

    public List<GameObject> activeBricks;   // active bricks


    public Color[] brickColors = new Color[] {
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
        activeBricks.Remove(brick.gameObject);
        if (activeBricks.Count == 0)
        {
            //Display Jordan's victory screen;
            Debug.Log("YOU WIN!");
        }
    }

    void Start()
    {
        // link prefab if not already linked
        if (BrickObject == null)
        {
            BrickObject   = Resources.Load("Prefabs/Brick", typeof(GameObject)) as GameObject;
            if (BrickObject == null)
            {
                Debug.Log("Prefab 'Brick' not found.");
                return;
            }
        }

        float x0 = -12f;
        float y0 =  7f;
        float brick_h = 0.5f;
        float brick_w = 2f;

        // instantiate child brick g.o.s
        for (int i=0; i<13; i++)
        {
            for (int j=0; j<7; j++)
            {
                GameObject brick = Instantiate(BrickObject);

                float x = x0 + i * brick_w;
                float y = y0 - j * brick_h;

                brick.transform.position = new Vector2(x, y);
                brick.transform.localScale = new Vector2(brick_w-0.1f, brick_h-0.1f);

                SpriteRenderer rend = brick.GetComponent<SpriteRenderer>();
                rend.material.color = brickColors[j];
                rend.enabled = true; // show

                brick.transform.parent  = transform;  // set Bricks as parent to this brick

                activeBricks.Add(brick);
            }
        }
    }

    void Update()
    {

    }
}
