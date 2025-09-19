using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Base : BaseController
{
    public ItemData _data;
    protected TileData curTile;
    public override bool Init()
    {
        if(base.Init() == false)    
            return false;

        
        return true;
    }
    public abstract void ItemAbility();
}
