using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPop : UI_Pop
{
    enum Objects
    {
        TopContent,
        BottomContent,
    }
    enum Images
    {
        ItemImage,
        Grow,
        Water,
        GrowSlider,
        WaterSlider,
    }
    enum Texts
    {
        Name_Txt,
        Weather_Txt,
        Grow_Txt,
        Humidity_Txt,
        GorwSlider_Txt,
        Water_Txt,
        Explan_Txt
    }
    enum Buttons
    {
        Close_Btn,
    }
    private TileData _tile;
    private string weather_Txt ="";
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Close_Btn).gameObject.BindEvent(ClosePopupUI);


        WaterAction(_tile.Water, 100);
        Refresh();
        return true;
    }

    public void SetInfo(TileData tile)
    {
        _tile = tile;
    }

    public void Refresh()
    {
        if(_tile.Plant == null)
        {
            GetObject((int)Objects.TopContent).gameObject.SetActive(false);
            GetImage((int)Images.GrowSlider).gameObject.SetActive(false);
        }
        else
        {
            CropsData crops = _tile.Plant._data;
            GetWeahterText(crops);

            GetImage((int)Images.ItemImage).sprite = crops.PlantImage;
            GetText((int)Texts.Name_Txt).text = $"이름:{crops.PlantName}";
            GetText((int)Texts.Weather_Txt).text = $"날씨 조건:{weather_Txt}";
            GetText((int)Texts.Grow_Txt).text = $"성장시간:{crops.MinGrowTime}~{crops.MaxGrowTime}";
            GetText((int)Texts.Humidity_Txt).text = $"습도 조건:{crops.MinHumidity}~{crops.MaxHumidity}";

            _tile.Plant.growAction = ExpAction;

            _tile.Plant.stringText = GetText((int)Texts.Explan_Txt);
            _tile.Plant.stringAction = StringAction;
        }
        _tile.waterAction = WaterAction;

    }
    private void ExpAction(float cur, float max)
    {
        GetImage((int)Images.Grow).fillAmount = cur/max;
        if(cur >= max)
            GetText((int)Texts.GorwSlider_Txt).text = $"성장완료";
        else
            GetText((int)Texts.GorwSlider_Txt).text = $"{cur}/{max} 성장 중..";
    }
    private void WaterAction(float cur, float max)
    {
        if(GetImage((int)Images.Water) == null)
            return;

        GetImage((int)Images.Water).fillAmount = cur/max;
        GetText((int)Texts.Water_Txt).text = $"{cur}%";
    }
    private void StringAction(string text)
    {
        GetText((int)Texts.Explan_Txt).text = text;
    }
    private void GetWeahterText(CropsData type)
    {
        Define.Weather[] weathers = type.Wheathers;
        foreach(Define.Weather weather in weathers)
        {
            switch (weather)
            {
                case Define.Weather.Clear:
                    weather_Txt += "맑음, ";
                    break;
                case Define.Weather.Cloudy:
                    weather_Txt += "흐림,";
                    break;
                case Define.Weather.Hail:
                    weather_Txt += "우박,";
                    break;
                case Define.Weather.Rainy:
                    weather_Txt += "비,";
                    break;
                case Define.Weather.Stormy:
                    weather_Txt += "폭풍,";
                    break;
            }
                
        }
    }

}
