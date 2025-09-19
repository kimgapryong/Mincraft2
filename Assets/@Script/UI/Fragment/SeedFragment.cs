using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFragment : UI_Base
{
    enum Type
    {
        Plant,
        Seed,
    }

    enum Images
    {
        PlantImage
    }
    enum Texts
    {
        Plant_Txt,
        Explation_Txt,
        Count_Txt,
    }
    enum Buttons
    {
        Single_Btn,
        All_Btn,
    }

    private Type curType;
    private CropsData _data;
    private int _count;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        return true;
    }
    public void SetSeedType()
    {
        curType = Type.Seed;
    }
    public void SetPlantType()
    {
        curType = Type.Plant;
    }
    public void Refresh()
    {
        
    }
    
}
