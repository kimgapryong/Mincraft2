using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CropsData", fileName ="New CropsData")]
public class CropsData : ScriptableObject
{
    public Define.Crops Type;
    public Define.Weather[] Wheathers;
    public string Explation;
    public float MinHumidity;
    public float MaxHumidity;
    public int MinGrowTime;
    public int MaxGrowTime;
    public float Time;
    public int Price;

    [Header("�Ĺ�")]
    public string PlantName;
    public Sprite PlantImage;
    public Sprite Grow1;
    public Sprite Grow2;
    public Sprite Grow3;

    [Header("����")]
    public string SeedName;
    public Sprite SeedImage;
}
