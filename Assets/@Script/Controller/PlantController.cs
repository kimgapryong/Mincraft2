using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : BaseController
{
    public CropsData _data;
    private TileData _tile;

    private Define.Weather[] weathers;

    public Action<float, float> growAction;
    public Action<string> stringAction;
    public Text stringText;
    private SpriteRenderer sp;

    private bool _cantGrow;
    public bool Finish;

    private float maxTime;
    private float _grow;
    public float Grow
    {
        get { return _grow; }
        set
        {
            _grow = value;
            Switch(value);
        }
    }

    public override bool Init()
    {
        if(base.Init() == false) 
            return false;

        sp = GetComponent<SpriteRenderer>();
        sp.sprite = _data.Grow1;

        StartCoroutine(StartGrow());
        return true;
    }

    public void SetInfo(CropsData data, TileData tile)
    {
        _data = data;
        _tile = tile;
        weathers = data.Wheathers;
        maxTime = data.Time;

        tile.Plant = this;
    }
    private void Switch(float cur)
    {
        float value = cur / maxTime;
        if (value >= 0.8f)
            sp.sprite = _data.Grow3;
        else if (value >= 0.5f)
            sp.sprite = _data.Grow2;
        else
            sp.sprite = _data.Grow1;
    }

    private IEnumerator StartGrow()
    {
        while(Grow < maxTime)
        {
            while (_cantGrow)
            {
                if (stringText != null)
                    stringText.gameObject.SetActive(true);

                yield return null;
            }

            if(stringText != null)
                stringText.gameObject.SetActive(false);
            Grow += _tile.GrowPoint;
            growAction?.Invoke(Grow, maxTime);
            yield return new WaitForSeconds(1f);
        }
        Finish = true;

    }
}
