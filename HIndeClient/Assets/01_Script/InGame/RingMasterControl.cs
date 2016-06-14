using UnityEngine;
using System.Collections;

public class RingMasterControl : MonoBehaviour 
{
    public float velocity;
    public float RageSpeedRate;
    public bool IsView;
    public Transform FirePos;
    public Rigidbody2D WebProj;
    public Animator anim;

    public void Init()
    {
        IsView = false;
        anim = GetComponent<Animator>();
        anim.Play("Run");
        StartCoroutine(TempFire());
    }

    void Update()
    {
        if (IG_Manager.Instance.IsPause || IG_Manager.Instance.IsGameOver) return;

        IsView = Camera.main.WorldToScreenPoint(transform.position).x > 0;
        
        if (IG_Manager.Instance.AnimalCon.IsStopped == false)
        {
            transform.Translate(new Vector2(velocity - (Common.BasicVelocity * IG_Manager.Instance.SpeedRate), 0) * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            transform.Translate(new Vector2(velocity * RageSpeedRate, 0) * Time.fixedDeltaTime, Space.World);
        }
    }

    public void End()
    {
        anim.Play("End");
    }

    public void FireWeb()
    {
        if (IsView)
        {
            WebProj.transform.position = FirePos.position;
            WebProj.gameObject.SetActive(true);
            WebProj.AddForce((IG_Manager.Instance.TargetPos.localPosition - IG_Manager.Instance.AnimalCon.transform.localPosition).normalized * 50);
        }
        else
        {
            WebProj.transform.position = IG_Manager.Instance.FirePos.position;
            WebProj.gameObject.SetActive(true);
            WebProj.AddForce((IG_Manager.Instance.TargetPos.localPosition - IG_Manager.Instance.AnimalCon.transform.localPosition).normalized * 50);
        }
    }

    IEnumerator TempFire()
    {
        while (true)
        {
            if (IG_Manager.Instance.IsGameOver) yield break;
            while (IG_Manager.Instance.IsPause) yield return null;

            IG_Manager.Instance.ViewManager.SetActiveWarning(true);
            yield return new WaitForSeconds(1);
            IG_Manager.Instance.ViewManager.SetActiveWarning(false);

            FireWeb();
            yield return new WaitForSeconds(10);
        }
    }
}
