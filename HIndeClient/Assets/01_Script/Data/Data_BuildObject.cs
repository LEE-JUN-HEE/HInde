using System;
using System.Collections.Generic;

public class Data_BuildObject : Data_Object
{
    public Data_BuildObject(Object[] param)
    {
        PosType = (Common.PosType)(int.Parse((string)param[0]));
        Pos_x = float.Parse((string)param[1]);
    }
}
