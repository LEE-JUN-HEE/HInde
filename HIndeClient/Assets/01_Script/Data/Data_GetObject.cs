using System;
using System.Collections.Generic;

public class Data_GetObject : Data_Object
{
    public Common.GetType GetType;
    public float Value;

    public Data_GetObject(Object[] param)
    {
        PosType = (Common.PosType)(int.Parse((string)param[0]));
        Pos_x = float.Parse((string)param[1]);
        GetType = (Common.GetType)int.Parse((string)param[5]);
        Value = float.Parse((string)param[6]);
    }
}
