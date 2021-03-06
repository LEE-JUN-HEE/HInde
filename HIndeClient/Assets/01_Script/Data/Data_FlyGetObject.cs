﻿using System;
using System.Collections.Generic;

public class Data_FlyGetObject : Data_Object
{
    public float Velocity;
    public UnityEngine.Vector2 Direction;
    public Common.GetType GetType;
    public float Value;
    //public Common.Direction Direction;

    public Data_FlyGetObject(Object[] param)
    {
        PosType = (Common.PosType)(int.Parse((string)param[0]));
        Pos_x = float.Parse((string)param[1]);
        Velocity = float.Parse((string)param[3]);
        switch ((Common.Direction)int.Parse((string)param[4]))
        {
            case Common.Direction.Left:
                Direction = new UnityEngine.Vector2(-Velocity, 0);
                break;
            case Common.Direction.Right:
                Direction = new UnityEngine.Vector2(Velocity, 0);
                break;
            case Common.Direction.Up:
                Direction = new UnityEngine.Vector2(0, Velocity);
                break;
            case Common.Direction.Down:
                Direction = new UnityEngine.Vector2(0, -Velocity);
                break;
        }
        GetType = (Common.GetType)int.Parse((string)param[5]);
        Value = float.Parse((string)param[6]);
    }
}
