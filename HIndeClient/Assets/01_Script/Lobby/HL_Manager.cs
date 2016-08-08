using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HL_Manager : MonoBehaviour 
{
    static public HL_Manager Instance;

    public GameObject LobbyUI;
    public GameObject Animal;
    public Animator RingMaster;
    public Animator Elephant;
    public GameObject GO_ExitPopup;

    bool isPopup = false;
    bool isringmalookright = true;

    void Start()
    {
        Instance = this;
        Elephant.Play("00_Hurt");
        Elephant.SetBool("Hurt", true);
        RingMaster.transform.localRotation = new Quaternion(0, 180, 0, 0);
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
        Elephant.SetBool("Hurt", false);
        StartCoroutine(StartChase());
        StartCoroutine(FadeOutBGM());
        Animal.GetComponent<AudioSource>().Play();
        while (Camera.main.WorldToViewportPoint(Animal.transform.position).x < 1.2)
        {
            Animal.transform.Translate(2.5f * Time.fixedDeltaTime, 0, 0);
            yield return null;
        }

        //페이드아웃 넣어도되고
        yield break;
    }

    IEnumerator StartChase()
    {
        while (Camera.main.WorldToViewportPoint(Animal.transform.position).x < 0.6)
            yield return null;

        Animal.GetComponent<AudioSource>().Stop();
        RingMaster.transform.parent.GetComponent<AudioSource>().Play();
        RingMaster.transform.localRotation = new Quaternion(0, -0, 0, 0);
        RingMaster.GetComponent<TweenPosition>().enabled = false;
        RingMaster.Play("Start");
        yield return new WaitForSeconds(1.0f);
        while (Camera.main.WorldToViewportPoint(RingMaster.transform.position).x < 1.2)
        {
            RingMaster.transform.parent.Translate(3 * Time.fixedDeltaTime, 0, 0);
            yield return null;
        }

        SceneManager.LoadScene("Loading");
        yield break;
    }

    IEnumerator FadeOutBGM()
    {
        while (GetComponent<AudioSource>().volume > 0)
        {
            GetComponent<AudioSource>().volume -= 0.05f;
            yield return null;
        }
        yield break;
    }

    public void RingmaTurn()
    {
        if (isringmalookright)
        {
            RingMaster.transform.localRotation = new Quaternion(0, 0, 0, 0);
            RingMaster.GetComponent<TweenPosition>().PlayReverse();
        }
        else
        {
            RingMaster.transform.localRotation = new Quaternion(0, 180, 0, 0);
            RingMaster.GetComponent<TweenPosition>().PlayForward();
        }
        isringmalookright = !isringmalookright;
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
        if (Social.localUser.authenticated == false)
        {
            Debug.Log("Not Login");
            return;
        }

        Social.ShowLeaderboardUI();
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

    public void OnClick_ExitCancel()
    {
        GO_ExitPopup.SetActive(false);
    }
}
