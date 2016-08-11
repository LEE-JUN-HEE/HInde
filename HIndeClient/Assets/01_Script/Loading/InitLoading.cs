using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;


/*
 * 최초 로딩화면
 * 로컬 정보를 읽어 메모리에 저장하는 클래스
 */

public class InitLoading : MonoBehaviour
{
    public UILabel LB_Text = null;
    public GameObject Login_Popup = null;

    bool isPopup = false;
    bool isLoggined = false;
    bool isLoginCancel = false;
    bool isComplete = false;// 로딩 완료
    bool isClick = false;   // 로딩 완료 후 클릭

    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        ////맵 데이터 리스트
        //Local_DB.MapData.Add(ParseMap("Map/Stage1"));
        //Local_DB.MapData.Add(ParseMap("Map/Stage1"));
        //Local_DB.MapData.Add(ParseMap("Map/Stage1"));
        //

        //환경설정
        //

        //구글 로그인
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(Login);

        while (isLoggined == false && isLoginCancel == false)
        {
            yield return null;
        }

        //유저정보 읽어오기(서버 연동이면 서버작업, 로컬이면 로컬작업)
        //

        //Admob
        BannerView banner = new BannerView("ca-app-pub-5325622833123971/8451261244", AdSize.Banner, AdPosition.TopLeft);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
        banner.Show();

        isComplete = true;
        LB_Text.text = "로딩 완료. 계속하려면 터치하세요";

        while (isClick == false)
        {
            yield return null;
        }

        SceneManager.LoadScene("Lobby");
        yield break;
    }


    void Login(bool success)
    {
        isLoggined = success;
        Login_Popup.SetActive(!isLoggined);
    }

    //IEnumerator LoginPopup(bool success)
    //{
    //    isPopup = true;
    //    Login_Popup.SetActive(true);

    //    while (isPopup == true)
    //    {
    //        yield return null;
    //    }

    //    isPopup = false;
    //    Login_Popup.SetActive(false);
    //    yield break;
    //}

    Data_Map ParseMap(string path)
    {
        Data_Map ret = new Data_Map();
        TextAsset TA = Resources.Load(path) as TextAsset;

        string[] read = TA.text.Split('\n');

        for (int i = 0; i < read.Length; i++)
        {
            Data_Object temp;
            string[] split = read[i].Split('&');

            if (split.Length == 1) continue;

            switch ((Common.ObjectType)int.Parse(split[2]))
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
            ret.AddData(temp);
        }
        return ret;
    }

    public void OnClick_Continue()
    {
        if (isComplete == false) return;

        isClick = true;
    }

    public void OnClick_ReLogin()
    {
        Login_Popup.SetActive(false);
        Social.localUser.Authenticate(Login);
    }

    public void OnClick_LoginCancel()
    {
        Login_Popup.SetActive(false);
        isLoginCancel = true;
    }
}
