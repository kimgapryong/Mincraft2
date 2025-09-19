using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Item_Base
{
    public int Use { get; set; }

    public override bool Init()
    {
        if(base.Init() == false)
            return false;
        
        Use = _data.UseCount;
        return true;
    }
    public override void ItemAbility()
    {
        if(Use == 0) return;

        curTile = Manager.Input.curTile;
        curTile.Water += _data.Percent;
        Use--;
    }

   
}
