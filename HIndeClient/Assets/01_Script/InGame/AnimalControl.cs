using UnityEngine;
using System.Collections;

public class AnimalControl : MonoBehaviour
{
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

    public bool IsStart { get; set; }
    public bool IsUp { get; set; }
    public bool IsJumping { get; set; }
    public bool IsPower { get; set; }
    public bool IsRunning { get; set; }
    public float JumpForce = 0;
    public float GravityScale = 0;
    Animator anim;
    Rigidbody2D rigid;

    public void Init()
    {
        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
        IsUp = true;

        //temp
        anim.Play("Walk");
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
            transform.Rotate(180, 0, 0);
            rigid.gravityScale = - GravityScale;
            IsUp = false;
        }
        else
        {
            transform.localRotation = Quaternion.identity;
            rigid.gravityScale = GravityScale;
            IsUp = true;
        }
    }

    public void Jump()
    {
        if (IsJumping) return;

        IsJumping = true;
        rigid.velocity = new Vector2(0, IsUp ? JumpForce : -JumpForce);

        //temp
        //anim.SetTrigger("Jump");
    }

    public void Running()
    {
        //temp
        anim.SetTrigger("Run");
    }

    public void Die()
    {

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
        Debug.Log(col.name + " / " + col.tag);
        switch (col.tag)
        {
            case "BuildObject":
                OnCollide_Build();
                break;

            case "GetObject":
                OnCollide_Get(col.GetComponent<IG_Object>());
                break;
        }
    }

    void OnCollide_Ground()
    {
        IsJumping = false;

        //temp
        anim.Play("Walk");
    }

    void OnCollide_Get(IG_Object col)
    {
        col.GetClear();
        Data_GetObject data = col.Data as Data_GetObject;
        switch (data.GetType)
        {
            case Common.GetType.Gold:
                IG_Manager.Instance.CurrentGold += data.Value;
                break;

            case Common.GetType.HP:
                //IG_Manager.Instance.CurrentGold += data.Value;
                break;

            case Common.GetType.Speed:
                break;
        }
    }

    void OnCollide_Build()
    {
        IG_Manager.Instance.AnimalStop();
    }
}
