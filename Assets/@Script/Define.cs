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
                plantName = "���";
                break;
            case Crops.Corn:
                plantName = "������";
                break;
            case Crops.Wheat:
                plantName = "��";
                break;
        }
        if (value < 0.5f)
        {
            return $"���� {plantName}�� �ü��� ������ ���丷�� �����Ƚ��ϴ� �� �� ������ �Ǹ��ϴ� ���� ��õ�մϴ�";
        }
        else if (value < 1.4f)
        {
            return $"���� {plantName}�� �ü��� ���� �ö����ϴ� ���� �Ǹ��ص� ������ ��¼�� ���� ������ ������ ���� �ֽ��ϴ�";
        }
        else
        {
            return $"���� {plantName}�� �ü��� ���� �ʴ���� �����׿� ���� �Ĵ� ���� ��õ�մϴ� ���� �ü��� �� {value.ToString("F1")}��� �Դϴ�";
        }
    }
    public static string GetWeather(Weather type)
    {
        switch(type)
        {
            case Weather.Clear:
                return "����";
            case Weather.Cloudy:
                return "�帲";
            case Weather.Rainy:
                return "��";
            case Weather.Stormy:
                return "��ǳ";
            case Weather.Hail:
                return "���";
        }

        return null;
    }

    public static string GetWeatherString(Weather weather, string wea, float grow, float ham)
    {
        string weatherTxt = "";
        return weatherTxt = $"���� ������ {wea}�̰� ��� ���� ����Ʈ�� {grow}�̸� ���� ���� ������ {ham}�Դϴ�";
    }
}
