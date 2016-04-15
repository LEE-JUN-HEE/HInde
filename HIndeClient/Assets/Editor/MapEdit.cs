using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MapEdit : MonoBehaviour 
{
    static public GameObject ObejctPool;
    //public List<MapEdit> list = new List<MapEdit>();
    [MenuItem("MapEdit/Export")]
    static void Export()
    {
        string result = null;
        ObejctPool = GameObject.FindWithTag("EditorOnly");

        for (int i = 0; i < ObejctPool.transform.childCount; i++)
        {
            MapEditObject temp = ObejctPool.transform.GetChild(i).GetComponent<MapEditObject>();
            result += (int)temp.postype + "&";
            result += (float)temp.Pos_x + "&";
            result += (int)temp.objtype + "&";
            result += (float)temp.velocity + "&";
            result += (int)temp.direction + "&";
            result += (int)temp.gettype + "&";
            result += (float)temp.Value;
            result += "\n";
        }
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/Map/" + ObejctPool.name + ".bytes", result);
        Debug.Log(ObejctPool.name + " is Export Complete");
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
