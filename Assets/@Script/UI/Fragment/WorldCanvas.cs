using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvas : UI_Base
{
    
    enum Objects
    {
        Content,
    }
    enum Buttons
    {
        Close_Btn,
    }
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        GetButton((int)Buttons.Close_Btn).gameObject.BindEvent(() =>
        {
            Manager.Input.worldSeed = null;
            Destroy(gameObject);
        });
        CreateSeedContent();
        return true;
    }
    private void CreateSeedContent()
    {
        foreach(Define.Crops crops in System.Enum.GetValues(typeof(Define.Crops)))
        {
            SeedDatas? seed = Manager.Item.GetSeed(crops);
            if (seed == null)
                continue;

            SeedDatas data = seed.Value;
            if (data.Count == 0)
                continue;

            Manager.UI.MakeSubItem<SeedContent>(GetObject((int)Objects.Content).transform, callback: (fa) =>
            {
                fa.SetInfo(data);
            });
        }
    }
}
