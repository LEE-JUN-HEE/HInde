using UnityEngine;
using System;
using System.Collections.Generic;

public class Common
{
    static public int Playcnt30 = 0;
    static public int tutorial = 0;

    static public float BasicVelocity = 1;
    static public float StopTime = 1.5f;
    static public float WebStopTime = 2.5f;
    static public float RunSpeedRate = 3f;

    static public float BasicPos = 68;
    static public float Fly_pos_y = 200;
    static public float FullObj_y_Size = 350;
    static public float JumpObj_y_Size = 60;

    static public float Up_Pos_y = 26;
    static public float Down_Pos_y = -26;
    static public float Up_Full_Pos_y = 177.5f;
    static public float Down_Full_Pos_y = -177.5f;

    static public float Clear_Pos_x = -0.5f;
    static public float FlyClear_Pos_x = 1.5f;

    static public string Tag_Build = "BuildObject";
    static public string Tag_Get = "GetObject";
    static public string Tag_Fly = "FlyObject";

    static public string Sprite_Gold = "Item_coin";
    static public string Sprite_Booster = "Item_booster";

    static public string Sprite_UJ = "_UJ_";
    static public string Sprite_UF = "_UF_0";
    static public string Sprite_UFly = "_UFLY_0";
    static public string Sprite_UTD = "_UTD_0";

    static public string Sprite_DJ = "_DJ_0";
    static public string Sprite_DF = "_DF_0";
    static public string Sprite_DFly = "_DFLY_0";
    static public string Sprite_DTU = "_DTU_0";

    public enum PosType
    {
        Up_Fly = 0,
        Up_Full = 1,
        Up_Jump,
        Down_Fly,
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
