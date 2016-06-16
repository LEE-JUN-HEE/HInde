using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapEditObject : MonoBehaviour
{
    public bool isUp;
    public bool isFlow;
    public enum EditType
    {
        //Build
        JumpBuild,
        FlyBuild,
        FullBuild,

        //Get
        JumpGold,
        FlyGold,
        JumpSpeed,
        FlySpeed,
    }
    public UISprite SP_Sprite = null;
    public EditType editType;
    public Common.PosType postype { get; set; }
    public float Pos_x;
    public Common.ObjectType objtype { get; set; }
    public float velocity;
    public Common.Direction direction;
    public Common.GetType gettype { get; set; }
    public float Value;

    void Update()
    {
        switch (editType)
        {
            case EditType.FullBuild:
                postype = isUp ? Common.PosType.Up_Full : Common.PosType.Down_Full;
                objtype = Common.ObjectType.Build;
                velocity = Value = 0;
                direction = Common.Direction.None;
                gettype = Common.GetType.None;
                break;

            case EditType.JumpBuild:
                postype = isUp ? Common.PosType.Up_Jump : Common.PosType.Down_Jump;
                objtype = Common.ObjectType.Build;
                velocity = Value = 0;
                direction = Common.Direction.None;
                gettype = Common.GetType.None;
                break;

            case EditType.FlyBuild:
                postype = isUp ? Common.PosType.Up_Fly : Common.PosType.Down_Fly;
                objtype = isFlow ? Common.ObjectType.FlyBuild : Common.ObjectType.Build;
                gettype = Common.GetType.None;
                break;

            case EditType.JumpGold:
                postype = isUp ? Common.PosType.Up_Jump : Common.PosType.Down_Jump;
                objtype = Common.ObjectType.Get;
                gettype = Common.GetType.Gold;
                direction = Common.Direction.None;
                velocity = 0;
                break;

            case EditType.FlyGold:
                postype = isUp ? Common.PosType.Up_Fly : Common.PosType.Down_Fly;
                objtype = isFlow ? Common.ObjectType.FlyGet : Common.ObjectType.Get;
                gettype = Common.GetType.Gold;
                break;

            case EditType.JumpSpeed:
                postype = isUp ? Common.PosType.Up_Jump : Common.PosType.Down_Jump;
                objtype = Common.ObjectType.Get;
                gettype = Common.GetType.Speed;
                direction = Common.Direction.None;
                velocity = 0;
                break;

            case EditType.FlySpeed:
                postype = isUp ? Common.PosType.Up_Fly : Common.PosType.Down_Fly;
                objtype = isFlow ? Common.ObjectType.FlyGet : Common.ObjectType.Get;
                gettype = Common.GetType.Speed;
                break;

        }
        //위치
        float pos_y = 0;
        switch (postype)
        {
            case Common.PosType.Down_Full:
                pos_y = Common.Down_Full_Pos_y;
                break;

            case Common.PosType.Down_Jump:
                pos_y = Common.Down_Pos_y;
                break;

            case Common.PosType.Up_Jump:
                pos_y = Common.Up_Pos_y;
                break;
            case Common.PosType.Up_Full:
                pos_y = Common.Up_Full_Pos_y;
                break;

            case Common.PosType.Up_Fly:
                pos_y = Common.Fly_pos_y;
                break;

            case Common.PosType.Down_Fly:
                pos_y = -Common.Fly_pos_y;
                break;
        }
        transform.localPosition = new Vector2(transform.localPosition.x, pos_y);
        Pos_x = transform.localPosition.x;

        //이미지, 사이즈
        //SP_Sprite.height = (int)Common.JumpObj_y_Size;
        switch (objtype)
        {
            case Common.ObjectType.Build:
                if (postype == Common.PosType.Down_Full || postype == Common.PosType.Up_Full)
                {
                    //SP_Sprite.spriteName = Common.Sprite_Column;
                    SP_Sprite.height = (int)Common.FullObj_y_Size;
                }
                else
                {
                    //SP_Sprite.spriteName = Common.Sprite_Build;
                }
                velocity = 0;
                direction = Common.Direction.None;
                gettype = Common.GetType.None;
                Value = 0;
                break;

            case Common.ObjectType.Get:
                switch (gettype)
                {
                    case Common.GetType.Gold:
                        SP_Sprite.spriteName = Common.Sprite_Gold;
                        break;

                    case Common.GetType.Speed:
                        SP_Sprite.spriteName = Common.Sprite_Booster;
                        break;
                }
                velocity = 0;
                direction = Common.Direction.None;
                break;

            case Common.ObjectType.FlyBuild:
                //SP_Sprite.spriteName = Common.Sprite_Fly;
                gettype = Common.GetType.None;
                Value = 0;
                break;

            case Common.ObjectType.FlyGet: switch (gettype)
                {
                    case Common.GetType.Gold:
                        SP_Sprite.spriteName = Common.Sprite_Gold;
                        break;

                    case Common.GetType.Speed:
                        SP_Sprite.spriteName = Common.Sprite_Booster;
                        break;
                }
                break;
        }
    }
}
