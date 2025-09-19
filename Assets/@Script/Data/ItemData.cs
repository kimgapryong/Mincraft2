using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName ="New ItemData")]
public class ItemData : ScriptableObject
{
    public Define.Item Type;
    public string Path;
    public Sprite Image;
    public string ItemName;
    public int MaxCount;
    public float Percent;
    public string Explanation;
    public float Price;

    [Header("¹°»Ñ¸®°³")]
    public int UseCount;

}
