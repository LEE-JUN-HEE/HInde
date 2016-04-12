using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 로컬에서 읽어들인 데이터 컨테이너
 * 한번 로딩 후 변화 없음
 */
public class Local_DB
{
    static public List<Data_Map> MapData = new List<Data_Map>();

    static public Data_Map GetMapData(int _stage)
    {
        switch (_stage)
        {
            case 1:
                return MapData[0];
                
            case 2:
                return MapData[1];
            
            case 3:
                return MapData[2];
                
            default:
                return null;
        }
    }
}
