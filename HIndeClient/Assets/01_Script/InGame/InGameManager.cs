using UnityEngine;
using System.Collections;

public class InGameManager : MonoBehaviour 
{
    public static InGameManager Instance;

    public AnimalControl AnimalCon;
    public float CurrentScore;
    public float CurrentGold;
    public int CurrentStage;
    public float SpeedRate { get; set; }
    public bool IsGameOver { get; set;}
    public bool IsPause { get; set; }


    /* Method */
    void Start()
    {
        Instance = this;
    }

    public void GameOver()
    {

    }

    public void StageChange()
    {
        //비동기로 맵 로딩, 배경바꾸기
    }

    IEnumerator maploading() // 맵데이터 로딩
    {
        yield break;
    }


    /* Handler */

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

    }

    public void OnClick_Exit()
    {

    }

    public void OnClick_Retry()
    {

    }

    public void OnClick_Continuos()
    {

    }
}
