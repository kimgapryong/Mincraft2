using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TvPop : UI_Pop
{
    enum Buttons
    {
        Next_Btn,
        Current_Btn,
        Close_Btn
    }
    enum Texts
    {
        Weather_Txt,
        WeatherExplation_Txt,
        Carrot_Txt,
        Corn_Txt,
        Wheat_Txt,
    }
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        Current();
        Refresh();
        GetButton((int)Buttons.Current_Btn).gameObject.BindEvent(Current);
        GetButton((int)Buttons.Next_Btn).gameObject.BindEvent(Next);
        GetButton((int)Buttons.Close_Btn).gameObject.BindEvent(() => ClosePopupUI());
        return true;
    }
    
    private void Current()
    {
        WeatherDatas datas = GameManager.Instance._weatherQueue.Peek();
        GetText((int)Texts.Weather_Txt).text = $"¿À´ÃÀÇ ³¯¾¾: {datas.Wea}";
        GetText((int)Texts.WeatherExplation_Txt).text = datas.WeaExplation;
    }
    private void Next()
    {
        WeatherDatas datas = GameManager.Instance._weatherQueue.ElementAt(1);
        GetText((int)Texts.Weather_Txt).text = $"³»ÀÏÀÇ ³¯¾¾: {datas.Wea}";
        GetText((int)Texts.WeatherExplation_Txt).text = datas.WeaExplation;
    }

    private void Refresh()
    {
        WeatherDatas datas = GameManager.Instance._weatherQueue.Peek();
        GetText((int)Texts.Carrot_Txt).text = datas.GetButket(Define.Crops.Carrot).Explation;
        GetText((int)Texts.Corn_Txt).text = datas.GetButket(Define.Crops.Corn).Explation;
        GetText((int)Texts.Wheat_Txt).text = datas.GetButket(Define.Crops.Wheat).Explation;
    }
    

}
