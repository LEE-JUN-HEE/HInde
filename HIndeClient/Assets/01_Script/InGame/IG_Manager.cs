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
    public RingMasterControl RingMaCon;
    public AudioSource BGM;
    public Transform FirePos;
    public Transform TargetPos;

    public float CurrentScore;
    public float CurrentGold;
    public int CurrentStage;
    public Queue<IG_Object> MapQueue = new Queue<IG_Object>();
    public List<Texture> BGList = new List<Texture>();
    public List<Texture> GroundList = new List<Texture>();
    public List<AudioClip> BGMList = new List<AudioClip>();

    public float BasicSpeedRate;
    public float SpeedRate { get; set; }
    public bool IsGameOver { get; set; }// 게임오버 변수. 게임오버 판단에 사용.
    public bool IsPause { get; set; }   // 정지 변수. 일시정지에 사용.
    public bool IsStaging { get; set; } // 스테이지 진행중 변수. 맵 교체시 교체 완료 확인으로 사용
    public bool IsStart { get; set; }   // 시작여부 확인 변수.

    float StopTime = 0;
    float StopDuration = 0;
    float RunStartTime = 0;
    float RunDuration = 0;
    float CurSpeedRate = 0;

    /* Method */
    void Update()
    {
        StageCheck();
        StopCheck();
        GameOverCheck();
        ScoreCheck();
        RunCheck();
    }

    void Start()
    {
        //로딩들
        Instance = this;

        //선택정보를 가지고있는 클래스에서 정보 받아와서 캐릭터 로드.

        BasicSpeedRate = 1f;
        SpeedRate = BasicSpeedRate;
        IsPause = true;
        IsStaging = false;
        IsStart = false;
        CurrentStage = 1;

        AnimalCon.Init();
        RingMaCon.Init();
        ViewManager.Init();
        ObjectPool.Init();
        StageChange(CurrentStage);
    }

    void StageCheck()
    {
        //스테이지 진행경과 체크
        if (IsStaging == false) return;
        if (MapQueue.Peek().IsColandFly == true || MapQueue.Peek().Data == null)
        {
            MapQueue.Dequeue(); 
            if (MapQueue.Count == 0)
            {
                IsStaging = false;
                //if(CurrentStage >= 3)
                //{

                //}
                //else
                //{
                    CurrentStage += 1;
                    BasicSpeedRate += 0.3f;
                //}

                StageChange(CurrentStage);
            }
            return;
        }

        if (Camera.main.WorldToViewportPoint(MapQueue.Peek().transform.position).x < Common.Clear_Pos_x)
        {
            MapQueue.Dequeue().Clear();
            if (MapQueue.Count == 0)
            {
                IsStaging = false;
                CurrentStage += 1;//(CurrentStage >= 3) ? 0 : 1;
                StageChange(CurrentStage);
            }
        }
    }

    void StopCheck()
    {
        if (AnimalCon.IsStopped == false) return;

        if (StopTime + StopDuration < Time.fixedTime)
        {
            AnimalRecovery();
        }
    }

    void GameOverCheck()
    {
        if(IsGameOver) return;

        if (RingMaCon.transform.localPosition.x > AnimalCon.transform.localPosition.x - 100)
        {
            IsGameOver = true;
            AnimalCon.Die();
        }
    }

    void ScoreCheck()
    {
        if (IsPause || IsGameOver) return;

        CurrentScore += Common.BasicVelocity * Time.fixedDeltaTime * IG_Manager.Instance.SpeedRate * 5;
    }

    void RunCheck()
    {
        if(AnimalCon.IsRunning == false) return;

        if (RunStartTime + RunDuration < Time.fixedTime)
        {
            AnimalRunEnd();
        }
    }

    void Pause(bool isOn)
    {
        IsPause = isOn;
        if (isOn)
        {
            BGM.Pause();
        }
        else
        {
            BGM.Play();
        }
    }

    
    //////////////////////////////////////////

    public void GameOver()
    {
        IsGameOver = true;
        StartCoroutine(EndBGMFadeOut());
        RingMaCon.End();
        ViewManager.Popup(IG_ViewManager.PopupType.GameOver, true);
    }

    public void StageChange(int index)
    {
        StartCoroutine(maploading(index));
        //비동기로 맵 로딩, 배경바꾸기
    }

    public void Flip()
    {
        if (AnimalCon.IsStopped == true) return;

        AnimalCon.Flip();
    }

    public void Jump()
    {
        if (AnimalCon.IsStopped == true) return;

        AnimalCon.Jump();
    }

    //Animal 관련 로직중 'SpeedRate'와 '아이템 지속시간' 관련 로직은 IG_Manager에서 처리
    public void AnimalStop(float _duration)
    {
        if (AnimalCon.IsStopped == true) return;

        //CurSpeedRate = SpeedRate;
        SpeedRate = 0;
        AnimalCon.IsStopped = true;
        StopTime = Time.fixedTime;
        StopDuration = _duration;
    }

    public void AnimalRecovery()
    {
        SpeedRate = BasicSpeedRate;
        AnimalCon.IsStopped = false;
        AnimalCon.anim.SetBool("Hurt", false);
    }

    public void AnimalRun(float duration)
    {
        if (AnimalCon.IsRunning == false)
        {
            //CurSpeedRate = SpeedRate;
            SpeedRate = Common.RunSpeedRate;
            AnimalCon.anim.SetTrigger("Booster");
        }
        if(AnimalCon.IsRunning == false)
            AnimalCon.transform.localPosition = new Vector3(AnimalCon.transform.localPosition.x, AnimalCon.IsUp ?180f : -180f, AnimalCon.transform.localPosition.z);
        AnimalCon.IsRunning = true;

        RunStartTime = Time.fixedTime;
        RunDuration = duration;
    }

    public void AnimalRunEnd()
    {
        SpeedRate = BasicSpeedRate;
        AnimalCon.IsRunning = false;
        AnimalCon.anim.SetTrigger("Return");
        //Animalcon의 애니메이션 바꿔주는 메소드
    }

    IEnumerator maploading(int index) // 맵데이터 로딩
    {
        bool FadeOut = CurrentStage <= 3 && IsStart == true;
        if (FadeOut)
        {
            ViewManager.TX_BG.ForEach(x=> x.GetComponent<TweenColor>().PlayForward());
        }

        for (int i = 0; i < 20 + 15 * index; i++)
        {
            IG_Object obj = ObjectPool.GetObejct();
            obj.SetData(CreateMap(i));
            MapQueue.Enqueue(obj);
        }
            //Data_Map ObjectDataList = Local_DB.GetMapData(index);
            //for (int i = 0; i < ObjectDataList.Data.Count; i++)
            //{
            //    IG_Object obj = ObjectPool.GetObejct();
            //    obj.SetData(ObjectDataList.Data[i]);
            //    MapQueue.Enqueue(obj);
            //    yield return null;
            //}
        IsStaging = true;


        if (FadeOut)
        {
            yield return StartCoroutine(BGMFade(false));
            BGM.Stop();
            BGM.clip = BGMList[CurrentStage - 1];
            BGM.Play();
            StartCoroutine(BGMFade(true));

            ViewManager.TX_BG.ForEach(x=> x.mainTexture = BGList[CurrentStage - 1]);
            ViewManager.TX_Ground.ForEach(x=> x.mainTexture = GroundList[CurrentStage - 1]);
            ViewManager.TX_BG.ForEach(x=> x.GetComponent<TweenColor>().PlayReverse());
        }
        yield break;
    }

    IEnumerator BGMFade(bool isIn)
    {
        if (IsGameOver == true) yield break;

        float upvalue = 0.03f;
        for (float i = 0; i < 1; i += upvalue)
        {
            BGM.volume += isIn ? upvalue : -upvalue;
            yield return null;
        }
        yield break;
    }

    IEnumerator EndBGMFadeOut()
    {
        while (BGM.volume > 0)
        {
            BGM.volume -= 0.15f;
            yield return null;
        }

        yield break;
    }

    Data_Object CreateMap(int index)
    {
        Data_Object temp;

        int postype = Random.Range(0, 5);
        int xPos = 600 * index;
        int objtype = 0;

        int rand = Random.Range(0, 1000);
        if (rand > 900)
            objtype = 0;
        else if (rand > 700)
            objtype = 1;
        else
            objtype = 2;

        float veocity = Random.Range(0.5f, 2f);
        int direction = Random.Range(0, 2);
        int gettype = Random.Range(1, 2);
        int value = Random.Range(1, 10);

        string[] split = new string[7];
        split[0] = postype.ToString();
        split[1] = xPos.ToString();
        split[2] = objtype.ToString();
        split[3] = veocity.ToString();
        split[4] = direction.ToString();
        split[5] = gettype.ToString();
        split[6] = value.ToString();

        switch ((Common.ObjectType)objtype)
        {
            case Common.ObjectType.Get:
                temp = new Data_GetObject(split);
                break;
            case Common.ObjectType.Build:
                temp = new Data_BuildObject(split);
                break;
            case Common.ObjectType.FlyBuild:
                temp = new Data_FlyObject(split);
                break;
            case Common.ObjectType.FlyGet:
                temp = new Data_FlyGetObject(split);
                break;

            default:
                temp = new Data_Object();
                Debug.LogError("Null DataObject");
                break;
        }

        return temp;
    }

    /* Handler */
    public void OnClick_Start()
    {
        IsPause = false;
        IsStart = true;
        BGM.clip = BGMList[0];
        BGM.Play();
        ViewManager.Popup(IG_ViewManager.PopupType.Start, false);
    }

    public void OnClick_Pause()
    {
        Pause(true);
        ViewManager.Popup(IG_ViewManager.PopupType.Pause, true);
    }

    public void OnClick_Exit()
    {
        SceneManager.LoadScene("Lobby");
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
        Pause(false);
        ViewManager.Popup(IG_ViewManager.PopupType.Pause, false);
    }
}
