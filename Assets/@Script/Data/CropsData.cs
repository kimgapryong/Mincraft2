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

    [Header("½Ä¹°")]
    public string PlantName;
    public Sprite PlantImage;
    public Sprite Grow1;
    public Sprite Grow2;
    public Sprite Grow3;

    [Header("¾¾¾Ñ")]
    public string SeedName;
    public Sprite SeedImage;
}
