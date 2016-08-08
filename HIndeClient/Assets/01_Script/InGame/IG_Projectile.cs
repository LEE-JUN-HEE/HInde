using UnityEngine;
using System.Collections;

public class IG_Projectile : MonoBehaviour 
{
    public UITexture sp_web = null;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1.2f ||
            Camera.main.WorldToViewportPoint(transform.position).x < -0.2f ||
            Camera.main.WorldToViewportPoint(transform.position).y > 1.2f ||
            Camera.main.WorldToViewportPoint(transform.position).y < -0.2f)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void Collide()
    {
        sp_web.alpha = 0.0f;
    }

    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        sp_web.alpha = 1.0f;
    }
}
