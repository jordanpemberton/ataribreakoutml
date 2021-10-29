using UnityEngine;
using System.Collections.Generic;

public class BricksController : MonoBehaviour
{
    GlobalData d;

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
        d = GlobalData.GetInstance();

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

        // instantiate child brick g.o.s
        for (int i=0; i<(int)d.NUM_BRICKS_X; i++)
        {
            for (int j=0; j<(int)d.NUM_BRICKS_Y; j++)
            {
                GameObject brick = Instantiate(BrickObject);

                float x = d.BRICK_X0 + i * d.BRICK_W;
                float y = d.BRICK_Y0 - j * d.BRICK_H;

                brick.transform.position = new Vector2(x, y);

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
