using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    private Dictionary<Define.Item, ItemDatas> _itemDic = new Dictionary<Define.Item, ItemDatas>();
    public Dictionary<Define.Item, Action> _itemActionDic = new Dictionary<Define.Item, Action>();

    public Dictionary<Define.Crops, SeedDatas> _SeedDic = new Dictionary<Define.Crops, SeedDatas>();
    public Dictionary<Define.Crops, SeedDatas> _SeedHouseDic = new Dictionary<Define.Crops, SeedDatas>();

    public void AddItem(Item_Base itemBase)
    {
        Define.Item type = itemBase._data.Type;
        if(_itemDic.TryGetValue(type, out ItemDatas datas))
        {
            if(datas.Item._data.MaxCount >= datas.Count)
                return;

            datas.Count++;
            _itemDic[type] = datas;
        }
        else
        {
            Debug.Log("new Item");
            ItemDatas newItem = new ItemDatas() {Item = itemBase, Count = 1 };
            _itemDic.Add(type, newItem);
            _itemActionDic.Add(type, itemBase.ItemAbility);
        }

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        main.SlotAllRefresh();

    }
    public ItemDatas? GetItemData(Define.Item type)
    {
        if (_itemDic.TryGetValue(type, out ItemDatas data))
            return data;

        return null;
    }
    public void AddSeed(CropsData data)
    {
        Define.Crops type = data.Type;
        if(_SeedDic.TryGetValue(type, out SeedDatas datas))
        {
            datas.Count++;
            _SeedDic[type] = datas;
        }
        else
        {
            SeedDatas newSeed = new SeedDatas() {CropsData = data, Count = 1 };
            _SeedDic.Add(type, newSeed);
        }

    }
    public SeedDatas? GetSeed(Define.Crops type)
    {
        if (_SeedDic.TryGetValue(type, out SeedDatas data))
            return data;

        return null;
    }
    public bool UseSeed(Define.Crops type)
    {
        SeedDatas? seed = GetSeed(type);
        SeedDatas data = seed.Value;    
        CropsData crops = data.CropsData;

        if(data.Count == 0 || Manager.Input.curTile == null)
            return false;

        GameObject obj = Manager.Resources.Instantiate("Seed", Manager.Input.curVec + Vector3.one * .5f, Quaternion.identity);
        PlantController plant = obj.GetOrAddComponent<PlantController>();
        plant.SetInfo(crops, Manager.Input.curTile);


        data.Count--;
        _SeedDic[type] = data;

        if(data.Count == 0)
            return false;
        
        return true;
    }
 
}
public struct SeedDatas
{
    public CropsData CropsData;
    public int Count;
}
public struct PlantDatas
{
    public CropsData CropsData;
    public int Count;
}
public struct ItemDatas
{
    public Item_Base Item;
    public int Count;
}
