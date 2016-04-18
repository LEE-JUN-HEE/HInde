using UnityEngine;
using System.Collections;

/*
 * 흐름 클래스
 * 화면에서 우에서 좌로 흐르는 물체의 로직을 담당하며
 * velcity로 속도를 조절한다.
 */

public class IG_Flow : MonoBehaviour 
{
    public float velocity;
	// Update is called once per frame
    void Awake()
    {
        velocity = Common.BasicVelocity;
    }

	void Update () 
    {
        if (IG_Manager.Instance.IsPause || IG_Manager.Instance.IsGameOver) return;

        transform.Translate(-velocity * Time.fixedDeltaTime * IG_Manager.Instance.SpeedRate, 0, 0);
	}
}
