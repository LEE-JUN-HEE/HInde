using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IG_Manager : MonoBehaviour
{
    //Logic
    public static IG_Manager Instance;

    public IG_UIManager UIManager;
    public IG_ObjectPool ObjectPool;
    public AnimalControl AnimalCon;

    public float CurrentScore;
    public float CurrentGold;
    public int CurrentStage;
    public Queue<IG_Object> MapQueue = new Queue<IG_Object>();
    public List<IG_Object> SetObjectList = new List<IG_Object>();

    public float SpeedRate { get; set; }
    public bool IsGameOver { get; set; }
    public bool IsPause { get; set; }
    public bool IsStaging { get; set; }


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
        UIManager.Init();
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
        UIManager.Popup(IG_UIManager.PopupType.GameOver, true);
    }

    public void StageChange(int index)
    {
        StartCoroutine(maploading(Local_DB.GetMapData(index)));
        //비동기로 맵 로딩, 배경바꾸기
    }

    IEnumerator maploading(Data_Map ObjectDataList) // 맵데이터 로딩
    {
        for (int i = 0; i < ObjectDataList.Data.Count; i++)
        {
            IG_Object obj = ObjectPool.GetObejct();
            obj.SetData(ObjectDataList.Data[i]);
            MapQueue.Enqueue(obj);
            yield return null;
        }
        IsStaging = true;
        yield break;
    }


    /* Handler */
    public void OnClick_Start()
    {
        IsPause = false;
        UIManager.Popup(IG_UIManager.PopupType.Start, false);
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
        UIManager.Popup(IG_UIManager.PopupType.Pause, true);
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
        UIManager.Popup(IG_UIManager.PopupType.Pause, false);
    }
}
