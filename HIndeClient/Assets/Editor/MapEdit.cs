using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MapEdit : MonoBehaviour 
{
    static public GameObject[] ObejctPool;
    //public List<MapEdit> list = new List<MapEdit>();
    [MenuItem("MapEdit/Export")]
    static void Export()
    {
        string result = null;
        ObejctPool = GameObject.FindGameObjectsWithTag("EditorOnly");

        for (int i = 0; i < ObejctPool.Length; i++)
        {
            for (int j = 0; j < ObejctPool[i].transform.childCount; j++)
            {
                MapEditObject temp = ObejctPool[i].transform.GetChild(j).GetComponent<MapEditObject>();
                result += (int)temp.postype + "&";
                result += (float)temp.Pos_x + "&";
                result += (int)temp.objtype + "&";
                result += (float)temp.velocity + "&";
                result += (int)temp.direction + "&";
                result += (int)temp.gettype + "&";
                result += (float)temp.Value;
                result += "\n";
            } 
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Map/" + ObejctPool[i].name + ".bytes", result);
            result = null;
            Debug.Log(ObejctPool[i].name + " is Export Complete");
        }
    }
}
