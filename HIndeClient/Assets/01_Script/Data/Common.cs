using UnityEngine;
using System;
using System.Collections.Generic;

public class Common
{
    static public float Fly_pos_y = 10;
    static public float FullObj_y_Size = 100;
    static public float JumpObj_y_Size = 10;
    static public float Pos_y = 0;
   
    public enum PosType
    {
        Up_Fly = 0,
        Up_Full = 1,
        Up_Jump,
        Down_Fly = 10,
        Down_Full,
        Down_Jump,
    }

    public enum ObjectType
    {
        Get,
        Build,
        Fly,
    }

    public enum GetType
    {
        Gold,
        Speed,
        HP,
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }
}
