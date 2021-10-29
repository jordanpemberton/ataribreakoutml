using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalData
{
    // private singleton instance
    private static GlobalData instance;

    public float SCORE_H;
    public float WALL_W;
    public float PADDING;       // padding around scene
    public float BRICK_W;
    public float NUM_BRICKS_X;  // make float for division
    public float BRICKS_W;
    public float BRICK_H;
    public float NUM_BRICKS_Y;
    public float BRICKS_H;

    // // 2:3 screen ratio ie 400x600, 500x750, 800x1200
    // // center scene and camera at (0,0) origin
    public float SCENE_W;
    public float SCENE_H;
    // // bricks leftmost x and topmost y
    public float BRICK_X0;
    public float BRICK_Y0;

    // private singleton constructor
    private GlobalData()
    {
        // can be used like any other constructor to instantiate stuff (but should only get called once):
        SCORE_H  = 10f;
        WALL_W   = 3f;
        PADDING  = 2f;
        BRICK_W  = 5f;
        NUM_BRICKS_X = 13f;
        BRICKS_W = BRICK_W * NUM_BRICKS_X;
        BRICK_H  = 1.5f;
        NUM_BRICKS_Y = 7f;
        BRICKS_H = BRICK_H * NUM_BRICKS_Y;
        SCENE_W  = BRICKS_W + 2f * WALL_W + 2f * PADDING;
        SCENE_H  = SCENE_W  * 2f / 3f;
        BRICK_X0 = -SCENE_W / 2f + PADDING + WALL_W  + BRICK_W / 2f;
        BRICK_Y0 =  SCENE_H / 2f - PADDING - SCORE_H - WALL_W - BRICK_H / 2f;
    }

    // public static method to access private instance from anywhere!
    public static GlobalData GetInstance()
    {
        if (instance == null)
            instance = new GlobalData();
        return instance;
    }
}
