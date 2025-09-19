using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum State
    {
        Idle,
        Move,
    }
    public enum Item
    {
        None,   
        WaterCan,
        Homer,
        Auto,
        Net,
        House,
    }
    public enum Crops
    {
        Carrot,
        Corn,
        Wheat
    }
    public enum Weather
    {
        Clear,
        Cloudy,
        Rainy,
        Stormy,
        Hail,

    }

    public static string GetButketString(Crops crops, float value)
    {
        string plantName = "";
        switch (crops)
        {
            case Crops.Carrot:
                plantName = "당근";
                break;
            case Crops.Corn:
                plantName = "옥수수";
                break;
            case Crops.Wheat:
                plantName = "밀";
                break;
        }
        if (value < 0.5f)
        {
            return $"현재 {plantName}의 시세가 완전히 반토막이 나버렸습니다 좀 더 묶혀서 판매하는 것을 추천합니다";
        }
        else if (value < 1.4f)
        {
            return $"현재 {plantName}의 시세가 조금 올랐습니다 지금 판매해도 되지만 어쩌면 내일 가격이 폭등할 수도 있습니다";
        }
        else
        {
            return $"현재 {plantName}의 시세가 완전 초대박이 터졌네요 지금 파는 것을 추천합니다 현재 시세는 약 {value.ToString("F1")}배배 입니다";
        }
    }
    public static string GetWeather(Weather type)
    {
        switch(type)
        {
            case Weather.Clear:
                return "맑음";
            case Weather.Cloudy:
                return "흐림";
            case Weather.Rainy:
                return "비";
            case Weather.Stormy:
                return "폭풍";
            case Weather.Hail:
                return "우박";
        }

        return null;
    }

    public static string GetWeatherString(Weather weather, string wea, float grow, float ham)
    {
        string weatherTxt = "";
        return weatherTxt = $"현재 날씬는 {wea}이고 곡물의 성장 포인트는 {grow}이며 땅이 얻을 습도는 {ham}입니다";
    }
}
