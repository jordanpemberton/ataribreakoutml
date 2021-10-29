using UnityEngine;
using System.Collections.Generic;

public class WallsController : MonoBehaviour
{
    GlobalData d;

    public GameObject WallObject;

    public Color wallColor = Color.white;

    void Start()
    {
        d = GlobalData.GetInstance();

        // link prefab if not already linked
        if (WallObject == null)
        {
            WallObject   = Resources.Load("Prefabs/Wall", typeof(GameObject)) as GameObject;
            if (WallObject == null)
            {
                Debug.Log("Prefab 'Wall' not found.");
                return;
            }
        }

        float x, y, wall_len;

        // left wall
        GameObject left_wall = Instantiate(WallObject);
        x = -d.SCENE_W / 2f + d.PADDING + d.WALL_W / 2f;
        y = -d.SCORE_H / 2f;
        wall_len = d.SCENE_H - d.SCORE_H - d.PADDING * 2f;
        left_wall.transform.position   = new Vector2(x, y);
        left_wall.transform.localScale = new Vector2(d.WALL_W, wall_len);
        left_wall.GetComponent<SpriteRenderer>().material.color = wallColor;
        left_wall.transform.parent = transform;  // set Walls as parent to this wall

        // right wall
        GameObject right_wall = Instantiate(WallObject);
        x *= -1;
        right_wall.transform.position   = new Vector2(x, y);
        right_wall.transform.localScale = new Vector2(d.WALL_W, wall_len);
        right_wall.GetComponent<SpriteRenderer>().material.color = wallColor;
        right_wall.transform.parent = transform;  // set Walls as parent to this wall

        // top wall
        GameObject top_wall = Instantiate(WallObject);
        x = 0;
        y = d.SCENE_H / 2f - d.PADDING - d.SCORE_H - d.WALL_W / 2f;
        wall_len = d.SCENE_W - d.PADDING * 2f - d.WALL_W * 2f;
        top_wall.transform.position   = new Vector2(x, y);
        top_wall.transform.localScale = new Vector2(wall_len, d.WALL_W);
        top_wall.GetComponent<SpriteRenderer>().material.color = wallColor;
        top_wall.transform.parent = transform;  // set Walls as parent to this wall
    }
}
