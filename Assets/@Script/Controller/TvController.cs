using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvController : BaseController
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(player == null) 
            return;

        Manager.UI.ShowPopUI<TvPop>();
            
    }
}
