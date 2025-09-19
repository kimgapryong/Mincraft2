using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedContent : UI_Base
{
    enum Texts
    {
        SeedName,
        Explation_Txt,
    }
    enum Images
    {
        SeedImage,
    }
    enum Buttons
    {
        Plant_Btn
    }
    private Define.Crops type;
    private SeedDatas datas;
    private CropsData crops;
    protected override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        Refresh();

        GetButton((int)Buttons.Plant_Btn).gameObject.BindEvent(PlantBtn);
        return true;
    }
    public void SetInfo(SeedDatas datas)
    {
        this.datas = datas;
        crops = datas.CropsData;
        type = crops.Type;
    }
    public void Refresh()
    {
        GetText((int)Texts.SeedName).text = $"{crops.SeedName} X{datas.Count}";
        GetImage((int)Images.SeedImage).sprite = crops.SeedImage;
        GetText((int)Texts.Explation_Txt).text = crops.Explation;
    }
    private void PlantBtn()
    {
        if (!Manager.Item.UseSeed(type))
            Destroy(gameObject);

        datas = Manager.Item.GetSeed(type).Value;
        Refresh();
    }
}
