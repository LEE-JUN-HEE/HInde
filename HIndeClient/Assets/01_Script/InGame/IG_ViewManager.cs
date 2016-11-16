using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public PN_Help HelpUI;

    public UILabel LB_Score;
    public UILabel LB_Gold;
    public UILabel LB_Stage;
    public UILabel LB_StageAlert;
    public UILabel LB_Distance;
    public UILabel LB_StartInfo;

    public UILabel LB_Popup_Score;
    public UILabel LB_Popup_Gold;

    public UISprite SP_WebWarning;

    //InGame
    public List<UITexture> TX_BG;
    public List<UITexture> TX_Ground;
    public GameObject GO_FrontBG;

    float BackBGDist = 0;
    float FrontBGDist = 0;

    bool isInit = false;

    void Start()
    {
        SP_WebWarning.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInit == false) return;
        LB_Score.text = string.Format("{0}", (int)IG_Manager.Instance.CurrentScore);
        LB_Gold.text = ((int)IG_Manager.Instance.CurrentGold).ToString("D");
        LB_Stage.text = string.Format("{0:0} 단계", IG_Manager.Instance.CurrentStage + 1);
        LB_StageAlert.text = string.Format("{0:0} 단계", IG_Manager.Instance.CurrentStage + 1);
        DistanceCheck();
    }

    void FixedUpdate()
    {
        BGFlowCheck();
    }

    void DistanceCheck()
    {
        GO_Distance.gameObject.SetActive(!IG_Manager.Instance.RingMaCon.IsView);
        LB_Distance.text = string.Format("{0:0.0} m", (IG_Manager.Instance.AnimalCon.transform.position.x - IG_Manager.Instance.RingMaCon.transform.position.x) * 5);
    }

    void BGFlowCheck()
    {
        //BG
        for(int i = 0; i< 2; i++)
        {
            IG_BGLoop loopBG = TX_BG[i].GetComponent<IG_BGLoop>();
            if (loopBG.isNeedRefresh)
            {
                int other = (i == 0) ? 1 : 0;

                loopBG.transform.localPosition = new Vector2(TX_BG[other].transform.localPosition.x + loopBG.offset, 0);
                loopBG.isNeedRefresh = false;
            }

            IG_BGLoop loopRope = TX_Ground[i].GetComponent<IG_BGLoop>();
            if (loopRope.isNeedRefresh)
            {
                int other = (i == 0) ? 1 : 0;

                loopRope.transform.localPosition = new Vector2(TX_Ground[other].transform.localPosition.x + loopRope.offset, 0);
            }
        }

        //Rope

        //FrontBG
    }

    public void SetActiveWarning(bool value)
    {
        SP_WebWarning.gameObject.SetActive(value);
    }

    public void Init()
    {
        LB_StartInfo.text = string.Format("총 탈출 회수 : {0}회\n단장 거리 보너스 : x {1}", Common.Playcnt30, Mathf.Min(Common.Playcnt30, 30));
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
                LB_Popup_Score.text = string.Format("{0} M", (int)IG_Manager.Instance.CurrentScore);
                GO_Pause.SetActive(false);
                GO_Start.SetActive(false);
                GO_GameOver.GetComponent<AudioSource>().Play();
                break;
        }
    }
}
