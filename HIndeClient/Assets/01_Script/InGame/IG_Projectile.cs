using UnityEngine;
using System.Collections;

public class IG_Projectile : MonoBehaviour 
{
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(Camera.main.WorldToViewportPoint(transform.position));
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1.2f ||
            Camera.main.WorldToViewportPoint(transform.position).x < -0.2f ||
            Camera.main.WorldToViewportPoint(transform.position).y > 1.2f ||
            Camera.main.WorldToViewportPoint(transform.position).y < -0.2f)
        {
            gameObject.SetActive(false);
        }
    }
}
