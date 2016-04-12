using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IG_Manager : MonoBehaviour 
{
    public static IG_Manager Instance;

    public GameObject GO_Start;
    public GameObject GO_Pause;
    public GameObject GO_GameOver;
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
        IsPause = true;
        GO_Start.transform.parent.gameObject.SetActive(true);
        GO_Start.SetActive(true);
    }

    public void GameOver()
    {
        IsGameOver = true;
        GO_GameOver.transform.parent.gameObject.SetActive(true);
        GO_GameOver.SetActive(true);
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
    public void OnClick_Start()
    {
        GO_Start.SetActive(false);
        GO_Start.transform.parent.gameObject.SetActive(false);
        IsPause = false;
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
        GO_Pause.transform.parent.gameObject.SetActive(true);
        GO_Pause.SetActive(true);
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
        GO_Pause.transform.parent.gameObject.SetActive(false);
        GO_Pause.SetActive(false);
    }
}
