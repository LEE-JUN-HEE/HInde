using System;
using System.Collections.Generic;

public class Data_Map
{
    public List<Data_Object> Data = new List<Data_Object>();

    public void AddData(Data_Object _data)
    {
        Data.Add(_data);
    }
}