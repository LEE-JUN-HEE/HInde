using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InitLoading : MonoBehaviour 
{
    public UILabel LB_Text = null;
	void Start () 
    {
        StartCoroutine(Loading());
	}

    IEnumerator Loading()
    {
        //
        //로컬 파일 읽어와서 파싱후 데이터 생성
        //맵 데이터 리스트, 환경설정
        //

        //
        //유저정보 읽어오기(서버 연동이면 서버작업, 로컬이면 로컬작업)
        //

        //임시 맵정보
        Data_Map Mapdata = new Data_Map();
        for (int i = 0; i < 50; i++)
        {
            System.Object[] data = { (i % 6).ToString(), (100 * i).ToString(), (i % 3).ToString(), (i % 3).ToString(), 0.ToString(), (i % 2).ToString(), 10.ToString() };
            Data_Object temp;
            if (i % 3 == 0)
            {
                temp = new Data_GetObject(data);
            }
            else if (i % 3 == 1)
            {
                temp = new Data_BuildObject(data);
            }
            else
            {
                temp = new Data_FlyObject(data);
            }
            Mapdata.AddData(temp);
        }

        Local_DB.MapData.Add(Mapdata);
        Local_DB.MapData.Add(Mapdata);
        Local_DB.MapData.Add(Mapdata);
        //임시맵 정보 끝

        AsyncOperation scene = SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Additive);
        
        while (!scene.isDone)
        {
            LB_Text.text = scene.progress.ToString();
            yield return null;
        }
        SceneManager.UnloadScene("Init");        
        yield return null;
    }
}
