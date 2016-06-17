using UnityEngine;
using System.Collections;

public class IG_BGLoop : MonoBehaviour
{
    public float velocity;
    public float offset;
    public bool isNeedRefresh = false;

    void Awake()
    {
        //velocity = Common.BasicVelocity;
    }

    void Update()
    {
        if (IG_Manager.Instance.IsPause || IG_Manager.Instance.IsGameOver) return;

        transform.Translate(new Vector2(-velocity, 0) * Time.fixedDeltaTime * IG_Manager.Instance.SpeedRate);
        isNeedRefresh = transform.localPosition.x < - offset;
    }
}
