using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HL_Manager : MonoBehaviour 
{
    static public HL_Manager Instance;

    public GameObject LobbyUI;
    public GameObject Animal;
    public GameObject GO_ExitPopup;

    bool isPopup = false;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (isPopup == false)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GO_ExitPopup.SetActive(true);
            }
        }
    }

    IEnumerator StartLoad()
    {
        LobbyUI.SetActive(false);
        while (Camera.main.WorldToViewportPoint(Animal.transform.position).x < 1.2)
        {
            Animal.transform.Translate(2.5f * Time.fixedDeltaTime, 0, 0);
            yield return null;
        }

        //페이드아웃 넣어도되고
        SceneManager.LoadScene("Loading");
        yield break;
    }

    public void OnClick_Start()
    {
        StartCoroutine(StartLoad());
    }

    public void OnClick_Character()
    {

    }

    public void OnClick_Gold()
    {

    }

    public void OnClick_Rank()
    {

    }

    public void OnClick_Event()
    {

    }

    public void OnClick_Setting()
    {

    }

    public void OnClick_ExitApp()
    {
        Application.Quit();
    }

    public void OnClick_ExitCaccel()
    {
        GO_ExitPopup.SetActive(false);
    }
}
