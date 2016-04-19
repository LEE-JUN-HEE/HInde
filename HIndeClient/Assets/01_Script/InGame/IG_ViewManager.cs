using UnityEngine;
using System.Collections;

public class IG_ViewManager : MonoBehaviour
{
    public enum PopupType
    {
        Start,
        Pause,
        GameOver,
    }
    //UI
    public GameObject GO_PopupBlackBack;
    public GameObject GO_Start;
    public GameObject GO_Pause;
    public GameObject GO_GameOver;
    public GameObject GO_Distance;

    public UILabel LB_Score;
    public UILabel LB_Gold;
    public UILabel LB_Stage;
    public UILabel LB_Distance;

    public UILabel LB_Popup_Score;
    public UILabel LB_Popup_Gold;

    public UISprite SP_WebWarning;
    
    //InGame
    public UITexture TX_BG;
    public UITexture TX_Ground;
    bool isInit = false;

    void Start()
    {
        SP_WebWarning.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInit == false) return;
        LB_Score.text = string.Format("{0:0.0}", IG_Manager.Instance.CurrentScore);
        LB_Gold.text = ((int)IG_Manager.Instance.CurrentGold).ToString("D");
        LB_Stage.text = string.Format("{0:0} 스테이지",IG_Manager.Instance.CurrentStage);
        DistanceCheck();
    }

    void DistanceCheck()
    {
        if(IG_Manager.Instance.RingMaCon.IsView)
        {
            GO_Distance.gameObject.SetActive(false);
        }
        else
        {
            LB_Distance.text = string.Format("{0:0.0} m",(IG_Manager.Instance.AnimalCon.transform.position.x - IG_Manager.Instance.RingMaCon.transform.position.x) * 5);
        }
    }

    public void SetActiveWarning(bool value)
    {
        SP_WebWarning.gameObject.SetActive(value);
    }

    public void Init()
    {
        Popup(PopupType.Start, true);
        isInit = true;
    }

    public void Popup(PopupType type, bool isOpen)
    {
        GO_PopupBlackBack.SetActive(isOpen);
        switch (type)
        {
            case PopupType.Start:
                GO_Start.transform.parent.gameObject.SetActive(isOpen);
                GO_Start.SetActive(isOpen);
                GO_Pause.SetActive(false);
                GO_GameOver.SetActive(false);
                break;

            case PopupType.Pause:
                GO_Pause.transform.parent.gameObject.SetActive(isOpen);
                GO_Pause.SetActive(isOpen);
                GO_Start.SetActive(false);
                GO_GameOver.SetActive(false);
                break;

            case PopupType.GameOver:
                GO_GameOver.transform.parent.gameObject.SetActive(isOpen);
                GO_GameOver.SetActive(isOpen);
                LB_Popup_Gold.text = ((int)IG_Manager.Instance.CurrentGold).ToString("D");
                LB_Popup_Score.text = string.Format("{0:0.0} M", IG_Manager.Instance.CurrentScore);
                GO_Pause.SetActive(false);
                GO_Start.SetActive(false);
                break;
        }
    }
}
