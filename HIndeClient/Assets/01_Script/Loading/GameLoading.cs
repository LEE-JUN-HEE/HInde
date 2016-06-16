using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoading : MonoBehaviour
{
    public UILabel LB_Text;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(GameLoad());
    }

    IEnumerator GameLoad()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Additive);

        while (!scene.isDone)
        {
            LB_Text.text = string.Format("{0:0.0}%", scene.progress * 100);
            yield return null;
        }
        SceneManager.UnloadScene("Loading");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
