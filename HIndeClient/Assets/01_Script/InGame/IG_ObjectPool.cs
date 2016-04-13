using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class IG_ObjectPool : MonoBehaviour 
{
    public IG_Object InstantiateRef;
    List<IG_Object> ObjectList = new List<IG_Object>();

    public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            IG_Object temp = transform.GetChild(i).GetComponent<IG_Object>();
            if (temp != null)
            {
                ObjectList.Add(temp);
            }
        }
    }

    public IG_Object GetObejct()
    {
        IG_Object temp = ObjectList.Find(x => x.IsUse == false);

        if (temp == null)
        {
            GameObject Copy = GameObject.Instantiate(InstantiateRef.gameObject);
            IG_Object ret = Copy.GetComponent<IG_Object>();
            ObjectList.Add(ret);
            return ret;
        }
        else
        {
            return temp;
        }
    }

    public void ReturnObject(IG_Object _Obj)
    {
        _Obj.transform.parent = this.transform;
        _Obj.transform.localPosition = Vector2.zero;
    }
}
