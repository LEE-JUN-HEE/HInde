using UnityEngine;
using System.Collections;

public class IG_Object : MonoBehaviour 
{
    public Data_Object Data;
    public Common.ObjectType Type;
    public bool IsUse;

    void Update()
    {
        if (Data == null) return;

        if (IG_Manager.Instance.IsPause || IG_Manager.Instance.IsGameOver) return;

        if (Type == Common.ObjectType.Fly)
        {
            transform.Translate((Data as Data_FlyObject).Direction * Time.fixedDeltaTime);
        }
    }

    public void SetData(Data_Object data)
    {
        //위치 설정
        float pos_y = 0;
        switch (data.PosType)
        {
            case Common.PosType.Down_Full:
            case Common.PosType.Down_Jump:
            case Common.PosType.Up_Jump:
            case Common.PosType.Up_Full:
                pos_y = Common.Pos_y;
                break;

            case Common.PosType.Up_Fly:
                pos_y = Common.Fly_pos_y;
                break;

            case Common.PosType.Down_Fly:
                pos_y = -Common.Fly_pos_y;
                break;
        }

        //데이터 타입&태그 설정, 사이즈, 이미지 설정
        if (data is Data_BuildObject)
        {
            Type = Common.ObjectType.Build;
            tag = "Build";
        }
        else if (data is Data_GetObject)
        {
            Type = Common.ObjectType.Get;
            tag = "Get";
        }
        else if (data is Data_FlyObject)
        {
            Type = Common.ObjectType.Fly;
            tag = "Fly";
        }

        gameObject.SetActive(true);
        IsUse = true;
    }

    public void Clear()
    {
        IG_Manager.Instance.ObjectPool.ReturnObject(this);
        gameObject.SetActive(false);
        IsUse = false;
    }

    void SetBuild()
    {
        //타입 맞춰서 이미지 늘리기, 콜라이더 조정
    }

    void SetGet()
    {
        //이미지 변경
    }

    void SetFly()
    {
        //이미지 변경
    }
}
