using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 장애물 관리 클래스
 * 이미 생산한것을 Destroy 시키는 대신
 * 비활성화 시키고
 * 필요할때 활성화 시키는 형식의 클래스
 */
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
            Copy.transform.parent = this.transform;
            Copy.transform.localScale = Vector3.one;
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
