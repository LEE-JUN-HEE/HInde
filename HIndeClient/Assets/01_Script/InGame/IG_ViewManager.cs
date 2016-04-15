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

    public UILabel LB_Score;
    public UILabel LB_Gold;
    public UILabel LB_Stage;
    
    //InGame
    public UITexture TX_BG;
    public UITexture TX_Ground;
    bool isInit = false;

    void Update()
    {
        if (isInit == false) return;
        LB_Score.text = IG_Manager.Instance.CurrentScore.ToString("F1");
        LB_Gold.text = ((int)IG_Manager.Instance.CurrentGold).ToString("D");
        LB_Stage.text = string.Format("{0} 스테이지",IG_Manager.Instance.CurrentStage);
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
                GO_Pause.SetActive(false);
                GO_Start.SetActive(false);
                break;
        }
    }
}
