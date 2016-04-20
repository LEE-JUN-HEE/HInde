using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * 최초 로딩화면
 * 로컬 정보를 읽어 메모리에 저장하는 클래스
 */

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
            System.Object[] data = { (i % 6).ToString(),
                                       (100 * i).ToString(),
                                       (i % 3).ToString(),
                                       (0.5).ToString(),
                                       (2).ToString(),
                                       (i % 2 + 1).ToString(),
                                       10.ToString() };
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

        Local_DB.MapData.Add(ParseMap("Map/Stage1"));
        Local_DB.MapData.Add(ParseMap("Map/Stage1"));
        Local_DB.MapData.Add(ParseMap("Map/Stage1"));
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

    Data_Map ParseMap(string path)
    {
        Data_Map ret = new Data_Map();
        TextAsset TA = Resources.Load(path) as TextAsset;
        
        string[] read =  TA.text.Split('\n');

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
}
