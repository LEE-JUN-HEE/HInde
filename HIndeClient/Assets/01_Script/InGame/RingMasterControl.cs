using UnityEngine;
using System.Collections;

public class RingMasterControl : MonoBehaviour 
{
    public float velocity;
    public float RageSpeedRate;
    public bool IsView;
    public Transform FirePos;
    public Rigidbody2D WebProj;

    public void Init()
    {
        IsView = false;
        StartCoroutine(TempFire());
    }

    void Update()
    {
        if (IG_Manager.Instance.IsPause || IG_Manager.Instance.IsGameOver) return;

        IsView = Camera.main.WorldToScreenPoint(transform.position).x > 0;

        if (IG_Manager.Instance.IsAnimalStopped == false)
        {
            transform.Translate(new Vector2(velocity, 0) * Time.fixedDeltaTime * IG_Manager.Instance.SpeedRate);
        }
        else
        {
            transform.Translate(new Vector2(velocity + Common.BasicVelocity * RageSpeedRate, 0) * Time.fixedDeltaTime);
        }
    }

    public void FireWeb()
    {
        if (IsView)
        {
            WebProj.transform.position = FirePos.position;
            WebProj.gameObject.SetActive(true);
            WebProj.AddForce((IG_Manager.Instance.AnimalCon.transform.localPosition - transform.localPosition).normalized * 100);
        }
        else
        {
            WebProj.transform.position = IG_Manager.Instance.FirePos.position;
            WebProj.gameObject.SetActive(true);
            WebProj.AddForce((IG_Manager.Instance.AnimalCon.transform.localPosition - IG_Manager.Instance.FirePos.localPosition).normalized * 100);
        
        }
    }

    IEnumerator TempFire()
    {
        while (true)
        {
            if (IG_Manager.Instance.IsGameOver) yield break;
            while (IG_Manager.Instance.IsPause) yield return null;

            FireWeb();
            yield return new WaitForSeconds(3);
        }
    }
}
