using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapEditObject : MonoBehaviour
{
    public UISprite SP_Sprite = null;


    public Common.PosType postype;
    public float Pos_x;
    public Common.ObjectType objtype;
    public float velocity;
    public Common.Direction direction;
    public Common.GetType gettype;
    public float Value;

    void Update()
    {
        float pos_y = 0;
        switch (postype)
        {
            case Common.PosType.Down_Full:
            case Common.PosType.Down_Jump:
                pos_y = Common.Down_Pos_y;
                break;

            case Common.PosType.Up_Jump:
            case Common.PosType.Up_Full:
                pos_y = Common.Up_Pos_y;
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

        switch (objtype)
        {
            case Common.ObjectType.Build:
                SP_Sprite.spriteName = Common.Sprite_Build;
                velocity = 0;
                direction = Common.Direction.None;
                gettype = Common.GetType.None;
                Value = 0;
                break;

            case Common.ObjectType.Get:
                SP_Sprite.spriteName = Common.Sprite_Gold;
                velocity = 0;
                direction = Common.Direction.None;
                break;

            case Common.ObjectType.FlyBuild:
                SP_Sprite.spriteName = Common.Sprite_Build;
                gettype = Common.GetType.None;
                Value = 0;
                break;

            case Common.ObjectType.FlyGet:
                SP_Sprite.spriteName = Common.Sprite_Gold;
                break;
        }
    }
}
