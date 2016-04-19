using UnityEngine;
using System;
using System.Collections.Generic;

public class Common
{
    static public float BasicVelocity = 1;
    static public float StopTime = 1.5f;
    static public float RunSpeedRate = 2.5f;

    static public float Fly_pos_y = 200;
    static public float FullObj_y_Size = 600;
    static public float JumpObj_y_Size = 100;
    static public float Up_Pos_y = 50;
    static public float Down_Pos_y = -50;
    static public float Clear_Pos_x = -0.5f;
    static public float FlyClear_Pos_x = 1.5f;

    static public string Tag_Build = "BuildObject";
    static public string Tag_Get = "GetObject";
    static public string Tag_Fly = "FlyObject";
    static public string Sprite_Gold = "000_icon";
    static public string Sprite_Build = "012_icon_suriken";
   
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
        FlyBuild,
        FlyGet,
    }

    public enum GetType
    {
        None,
        Gold,
        Speed,
        HP,
    }

    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }
}
