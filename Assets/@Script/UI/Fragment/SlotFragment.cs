using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotFragment : UI_Base
{
    public Define.Item _type;
    private Image select;
    private MainCanvas _main;
    private Item_Base item;
    enum Images
    {
        ItemImage,
        Select_Bg,
    }
    enum Texts
    {
        Count_Txt,
    }
    protected override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        select = GetImage((int)Images.Select_Bg);
        select.gameObject.SetActive(false);

        GetImage((int)Images.ItemImage).gameObject.SetActive(false);
        GetText((int)Texts.Count_Txt).gameObject.SetActive(false);
        gameObject.BindEvent(ChangeSelect);

        _main.slot = true;
        return true;
    }
    public void SetInfo(MainCanvas main)
    {
        _main = main;
    }
    public void OnSelect()
    {
        if(item != null)
            item.gameObject.SetActive(true);
        select.gameObject.SetActive(true);
    }
    public void OffSelect()
    {
        if (item != null)
            item.gameObject.SetActive(false);
        select.gameObject.SetActive(false);
    }
    public void ChangeSelect()
    {
        foreach(var slot in _main._slotList)
            if(slot != this) slot.OffSelect();

        if(select.gameObject.activeSelf == true)
        {
            OffSelect();
            Manager.Input.curItem = Define.Item.None;
        }
        else
        {
            OnSelect();
            Manager.Input.curItem = _type;
        }

    }
    public void Refresh()
    {
        ItemDatas? data = Manager.Item.GetItemData(_type);
        if(data == null)
            return;

        ItemDatas itemData = data.Value;
        item = itemData.Item;
        if(itemData.Item._data.MaxCount != 1)
            GetText((int)Texts.Count_Txt).gameObject.SetActive(true);

        if(itemData.Count <= 0)
        {
            item = null;
            GetImage((int)Images.ItemImage).gameObject.SetActive(false);
            GetText((int)Texts.Count_Txt).gameObject.SetActive(false);
            return;
        }

        GetImage((int)Images.ItemImage).gameObject.SetActive(true);
        GetImage((int)Images.ItemImage).sprite = item._data.Image;
        GetText((int)Texts.Count_Txt).text = $"{itemData.Count}°³";
    }

}
