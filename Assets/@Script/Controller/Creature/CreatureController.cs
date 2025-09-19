using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    private Define.State _state;
    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            ChangeAnim(value);
        }
    }
    private Vector2 _dir;
    public Vector2 Dir
    {
        get { return _dir; }
        set { _dir = value;  ChangeAnim(State); }
    }

    public float moveSpeed;
    private string dirStr;
    protected Rigidbody2D rb;
    protected Animator anim;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        return true;
    }
    protected virtual void ChangeAnim(Define.State type)
    {
        string animStr = "";
        if(Dir != Vector2.zero)
        {
            dirStr = "Side";
            if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y))
            {
                if (Dir.y > 0)
                    dirStr = "B";
                else if (Dir.y < 0)
                    dirStr = "F";
            }
        }
     

        switch (type)
        {
            case Define.State.Idle:
                animStr = $"Idle_{dirStr}";
                anim.Play(animStr);
                break;
            case Define.State.Move:
                animStr = $"Walk_{dirStr}";
                anim.Play(animStr);
                break;
        }
    }
    private void Update()
    {
        UpdateMethod();
    }
    protected virtual void UpdateMethod()
    {
        float localY = (Dir.x > 0) ? -180 : (Dir.x < 0) ? 0 : transform.eulerAngles.y;
        Vector3 newPos = transform.eulerAngles;
        newPos.y = localY;
        transform.eulerAngles = newPos;
    }
}
