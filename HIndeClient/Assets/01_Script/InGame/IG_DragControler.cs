using UnityEngine;
using System.Collections;

public class IG_DragControler : MonoBehaviour {

    Vector2 StartPosition = Vector2.zero;

    void OnClick()
    {
        IG_Manager.Instance.Jump();
    }

    void OnDragStart()
    {
        StartPosition = UICamera.lastEventPosition;
    }

    void OnDragEnd()
    {
        if (UICamera.lastEventPosition.y > StartPosition.y && IG_Manager.Instance.AnimalCon.IsUp == false)
        {
            IG_Manager.Instance.Flip();
            return;
        }
        if (UICamera.lastEventPosition.y < StartPosition.y && IG_Manager.Instance.AnimalCon.IsUp == true)
        {
            IG_Manager.Instance.Flip();
            return;
        }
    }
}
