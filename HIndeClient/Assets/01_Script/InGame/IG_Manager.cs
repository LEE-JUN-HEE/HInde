using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IG_Manager : MonoBehaviour
{
    public static IG_Manager Instance;

    public IG_ViewManager ViewManager;
    public IG_ObjectPool ObjectPool;
    public AnimalControl AnimalCon;

    public float CurrentScore;
    public float CurrentGold;
    public int CurrentStage;
    public Queue<IG_Object> MapQueue = new Queue<IG_Object>();
    public List<Texture> BGList = new List<Texture>();
    public List<Texture> GroundList = new List<Texture>();

    public float SpeedRate { get; set; }
    public bool IsGameOver { get; set; }// 게임오버 변수. 게임오버 판단에 사용.
    public bool IsPause { get; set; }   // 정지 변수. 일시정지에 사용.
    public bool IsStaging { get; set; } // 스테이지 진행중 변수. 맵 교체시 교체 완료 확인으로 사용
    public bool IsStart { get; set; }


    /* Method */
    void Update()
    {
        StageCheck();
    }

    void Start()
    {
        Instance = this;

        SpeedRate = 1;
        IsPause = true;
        IsStaging = false;
        IsStart = false;
        ViewManager.Init();
        ObjectPool.Init();
        CurrentStage = 1;
        StageChange(CurrentStage);
    }

    void StageCheck()
    {
        //스테이지 진행경과 체크
        if (IsStaging == false) return;

        if (Camera.main.WorldToViewportPoint(MapQueue.Peek().transform.position).x < Common.Clear_Pos_x)
        {
            MapQueue.Dequeue().Clear();
            if (MapQueue.Count == 0)
            {
                IsStaging = false;
                CurrentStage += (CurrentStage >= 3) ? 0 : 1;
                StageChange(CurrentStage);
            }
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        ViewManager.Popup(IG_ViewManager.PopupType.GameOver, true);
    }

    public void StageChange(int index)
    {
        StartCoroutine(maploading(index));
        //비동기로 맵 로딩, 배경바꾸기
    }

    IEnumerator maploading(int index ) // 맵데이터 로딩
    {
        bool FadeOut = CurrentStage <= 3 && IsStart == true;
        if (FadeOut)
        {
            ViewManager.TX_BG.GetComponent<TweenColor>().PlayForward();
        }
        Data_Map ObjectDataList = Local_DB.GetMapData(index);
        for (int i = 0; i < ObjectDataList.Data.Count; i++)
        {
            IG_Object obj = ObjectPool.GetObejct();
            obj.SetData(ObjectDataList.Data[i]);
            MapQueue.Enqueue(obj);
            yield return null;
        }
        IsStaging = true;

        yield return new WaitForSeconds(1f);

        if (FadeOut) 
        {
            ViewManager.TX_BG.mainTexture = BGList[CurrentStage - 1];
            ViewManager.TX_Ground.mainTexture = GroundList[CurrentStage - 1];
            ViewManager.TX_BG.GetComponent<TweenColor>().PlayReverse();
        }

        yield break;
    }


    /* Handler */
    public void OnClick_Start()
    {
        IsPause = false;
        IsStart = true;
        ViewManager.Popup(IG_ViewManager.PopupType.Start, false);
    }

    public void OnClick_Flip()
    {
        AnimalCon.Flip();
    }

    public void OnClick_Jump()
    {
        AnimalCon.Jump();
    }

    public void OnClick_Pause()
    {
        IsPause = true;
        ViewManager.Popup(IG_ViewManager.PopupType.Pause, true);
    }

    public void OnClick_Exit()
    {
        Debug.Log("Exit");
    }

    public void OnClick_Setting()
    {
        Debug.Log("Setting");
    }

    public void OnClick_Retry()
    {
        Debug.Log("Retry");
        SceneManager.LoadScene("InGame");

    }

    public void OnClick_Feed()
    {
        Debug.Log("Feed");
    }

    public void OnClick_Continuos()
    {
        IsPause = false;
        ViewManager.Popup(IG_ViewManager.PopupType.Pause, false);
    }
}
