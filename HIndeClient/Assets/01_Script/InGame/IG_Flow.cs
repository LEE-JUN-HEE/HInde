using UnityEngine;
using System.Collections;

public class IG_Flow : MonoBehaviour 
{
    public float velocity;
	// Update is called once per frame
	void Update () 
    {
        if (IG_Manager.Instance.IsPause || IG_Manager.Instance.IsGameOver) return;

        transform.Translate(-velocity * Time.fixedDeltaTime, 0, 0);
	}
}
