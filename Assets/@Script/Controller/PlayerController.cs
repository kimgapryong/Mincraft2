using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    private Transform itemContent;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        State = Define.State.Idle;
        Manager.Player = this;

        itemContent = transform.Find("ItemContent");

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        main._acionQueue.Enqueue(() =>
        {
            foreach (var item in itemContent.GetComponentsInChildren<Item_Base>(true))
            {
                Debug.Log(item);
                Manager.Item.AddItem(item);
            }
        });
            

        return true;
    }
    private void FixedUpdate()
    {
        PlayerMove();
    }
    private void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Dir = new Vector2 (x, y).normalized;

        if(Dir == Vector2.zero)
        {
            State = Define.State.Idle;
            return;
        }

        if(State != Define.State.Move)
            State = Define.State.Move;
        rb.MovePosition(rb.position + Dir * moveSpeed * Time.fixedDeltaTime);
    }
}
