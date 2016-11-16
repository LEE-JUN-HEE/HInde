using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PN_Help : MonoBehaviour {

    public List<UIToggle> TGList = null;

    public void Show()
    {
        gameObject.SetActive(true);
        TGList[0].value = true;
    }

    public void OnClick_Close()
    {
        gameObject.SetActive(false);
    }
}
