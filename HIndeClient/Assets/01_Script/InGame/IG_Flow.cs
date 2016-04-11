using UnityEngine;
using System.Collections;

public class IG_Flow : MonoBehaviour 
{
    public float velocity;
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(-velocity * Time.fixedDeltaTime, 0, 0);
	}
}
