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
    public float initxpos = -20000f;
    public float normalxpos = -1000f;

    public void Init()
    {
        IsView = false;
        transform.localPosition = new Vector2(initxpos + normalxpos * Mathf.Min(Common.Playcnt30, 30) , transform.localPosition.y);
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
            transform.Translate(new Vector2((velocity * IG_Manager.Instance.BasicSpeedRate)  - (Common.BasicVelocity * IG_Manager.Instance.SpeedRate), 0) * Time.fixedDeltaTime, Space.World);
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
        WebProj.gameObject.SetActive(true);
        WebProj.GetComponent<IG_Projectile>().Fire();
        if (IsView)
        {
            WebProj.transform.position = FirePos.position;
            WebProj.AddForce((IG_Manager.Instance.TargetPos.localPosition - IG_Manager.Instance.AnimalCon.transform.localPosition).normalized * 50);
        }
        else
        {
            WebProj.transform.position = IG_Manager.Instance.FirePos.position;
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
