using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalControl : MonoBehaviour
{
    public enum sound
    {
        Jump = 0,
        Die = 1,
        Change = 2,
        Collide = 3,
        GetCoin = 4,
    }
    /*
     애니메이션 상황
     * Walk(Idle)
     * RunReady
     * Run // Hang하다가도 먹으면 자연스럽게 공으로 올라오도록
     * Flip
     * Jump
     * Hang
     * HangJump
     * Die
    */
    //Data변수 추가

    public bool IsUp { get; set; }
    public bool IsJumping { get; set; }
    public bool IsPower { get; set; }
    public bool IsRunning { get; set; }
    public bool IsStopped { get; set; }

    public List<AudioClip> SoundList = new List<AudioClip>();
    public AudioSource AnimalAudio;
    public float JumpForce = 0;
    public float GravityScale = 0;
    public Animator anim;
    Rigidbody2D rigid;
    CircleCollider2D col;
    bool isflip = false;

    public void Init()
    {
        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<CircleCollider2D>();
        IsUp = true;
        IsPower = false;
        IsStopped = false;
        IsJumping = false;

    }

    void Update()
    {
        anim.speed = IG_Manager.Instance.IsPause ? 0 : 1;

    }

    public void Flip()
    {
        if (IsJumping) return;

        if (IsUp)
        {
            //temp
            transform.localRotation = Quaternion.identity;
            transform.localPosition = new Vector3(transform.localPosition.x, -Common.BasicPos, transform.localPosition.z);
            transform.localRotation = new Quaternion(-180, 0, 0, 0);
            rigid.gravityScale = -1;
            IsUp = false;
        }
        else
        {
            transform.localRotation = Quaternion.identity;
            transform.localPosition= new Vector3(transform.localPosition.x, Common.BasicPos, transform.localPosition.z);
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            rigid.gravityScale = 1;
            IsUp = true;
        }
        PlaySound(sound.Change);
    }

    public void Jump()
    {
        if (IsJumping || isflip) return;

        anim.SetTrigger("Jump");
        PlaySound(sound.Jump);
        IsJumping = true;
        rigid.velocity = new Vector2(0, IsUp ? JumpForce : -JumpForce);
    }

    public void Running()
    {
        anim.SetTrigger("Booster");
    }

    public void Die()
    {
        anim.SetBool("Hurt", true);
    }

    public void PlaySound(sound index)
    {
        switch (index)
        {
            case sound.Jump:
            case sound.Change:
                AnimalAudio.clip = SoundList[(int)index];
                AnimalAudio.Play();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.collider.name + " / " + col.collider.tag);
        switch (col.collider.tag)
        {
            case "Ground":
                OnCollide_Ground();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.name + " / " + col.tag);
        switch (col.tag)
        {
            case "BuildObject":
                OnCollide_Build(col.GetComponent<IG_Object>());
                break;

            case "GetObject":
                OnCollide_Get(col.GetComponent<IG_Object>());
                break;

            case "Web":
                OnCollide_Web(col);
                break;
        }
    }

    void OnCollide_Ground()
    {
        IsJumping = false;

        if (IsRunning == false)
            anim.SetTrigger("Return");
    }

    void OnCollide_Get(IG_Object col)
    {
        col.GetClear();
        Data_GetObject data = col.Data as Data_GetObject;
        switch (data.GetType)
        {
            case Common.GetType.Gold:
                IG_Manager.Instance.CurrentGold += data.Value;
                col.PlaySound(sound.GetCoin, SoundList[(int)sound.GetCoin]);
                break;

            case Common.GetType.HP:
                //IG_Manager.Instance.CurrentGold += data.Value;
                break;

            case Common.GetType.Speed:
                IG_Manager.Instance.AnimalRun(data.Value);
                break;
        }
    }

    void OnCollide_Build(IG_Object col)
    {
        col.PlaySound(sound.Collide, SoundList[(int)sound.Collide]);
        if (IsRunning)
        {
            col.CollideGone();
            //장애물 튕김액션
        }
        else
        {
            IG_Manager.Instance.AnimalStop(Common.StopTime);
            anim.SetBool("Hurt", true);
        }
    }

    void OnCollide_Web(Collider2D col)
    {
        //잡히는 애니메이션
        col.gameObject.SetActive(false);
        if (IG_Manager.Instance.AnimalCon.IsRunning)
        {
            IG_Manager.Instance.AnimalRunEnd();
        }
        else
        {
            IG_Manager.Instance.AnimalStop(Common.WebStopTime);
            anim.SetBool("Hurt", true);
        }
        //IG_Manager.Instance.GameOver();
    }
}
