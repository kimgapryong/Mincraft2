using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStage : BaseController
{
    public List<CropsData> seedData;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        Manager.UI.ShowSceneUI<MainCanvas>();
        Manager.Tile.Init();

        foreach(CropsData data in seedData)
            Manager.Item.AddSeed(data);
        
        return true;
    }
}
