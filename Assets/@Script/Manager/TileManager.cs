using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager
{
    private const string MAIN_TILE = "Sprite_Tiles_Soil_23";
    private Vector3Int[] vectorArray = new Vector3Int[9] { Vector3Int.zero, Vector3Int.right, Vector3Int.left, Vector3Int.up, Vector3Int.down, new Vector3Int(1, 1), new Vector3Int(-1, 1), new Vector3Int(-1, -1), new Vector3Int(1, -1) };

    private int minX = 20;
    private int minY = -10;
    private int maxX = 40;
    private int maxY = 10;

    private Tilemap solid;
    public List<TileEx> _tileEx = new List<TileEx>();   

    public void Init()
    {
        int curValue = 0;
        GameObject grid = GameObject.Find("Grid");
        solid = grid.transform.Find("Solid").GetComponent<Tilemap>();

        for(int x = minX; x <= maxX; x++)
        {
            for(int y = minY; y <= maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                if(solid.GetTile(pos).name != MAIN_TILE)
                    continue;

                TileEx tile;
                GameObject tileObj = Manager.Resources.Instantiate("Tile/TileObject", (Vector3)pos + Vector3.one * 0.5f, Quaternion.identity);
                tile = new TileEx(tileObj);

                foreach(var vec in vectorArray)
                {
                    Vector3Int tileVec = (pos + vec);
                    TileData data = new TileData(solid, 50, tileVec, tile);
                    tile.SetTileData(tileVec, data);
                }
                _tileEx.Add(tile);
                curValue++;
            }
        }
        _tileEx[3].Lock = true;
    }

    public TileData GetTileData(Vector3Int vec)
    {
        TileData tile = null;
        foreach(TileEx ex in _tileEx)
        {
            tile = ex.GetTileData(vec);
            if (tile != null)
                return tile; 
        }

        return null;
    }
}
public class TileEx
{
    private GameObject obj;
    private bool _lock;
    public bool Lock
    {
        get { return _lock; }
        set
        {
            _lock = value;

            if(value)
                UnityEngine.Object.DestroyImmediate(obj);
        }
    }

    Dictionary<Vector3Int, TileData> _tileDic = new Dictionary<Vector3Int, TileData>();
    public void SetTileData(Vector3Int vecInt, TileData data)
    {
        _tileDic[vecInt] = data;
    }
    public TileData GetTileData(Vector3Int vecInt)
    {
        TileData tileData = null;
        if( _tileDic.TryGetValue(vecInt, out tileData) == false)
            return null;

        return tileData;
    }
    public void UpdateWater(float water)
    {
        foreach (var data in _tileDic.Values)
            data.Water += water;
    }       
    public void SetGrowPoint(float point)
    {
        foreach(var tile in _tileDic.Values)
            tile.GetGrowPoint(point);
    }
    public TileEx(GameObject obj)
    {
        this.obj = obj;
    }
}
public class TileData
{
    public TileEx parent;
    public Vector3Int vec;
    private Tilemap tilemap;

    public float GrowPoint { get; private set; } = 1f;
    public PlantController Plant { get; set; }

    public Action<float, float> waterAction;
    private float _water;
    public float Water
    {
        get { return _water; }
        set
        {
            _water = Mathf.Clamp(value, 0, 100);
            WaterChange(_water);
            waterAction?.Invoke(_water, 100);
        }
    }

    //Sprite_Tiles_Soil_9
    //Sprite_Tiles_Soil_23
    ///Sprite_Tiles_Soil_27
    private void WaterChange(float value)
    {
        if (value >= 80)
            tilemap.SetTile(vec, Manager.Resources.LoadTile("Sprite_Tiles_Soil_23"));
        else if( value >= 45)
            tilemap.SetTile(vec, Manager.Resources.LoadTile("Sprite_Tiles_Soil_27"));
        else
            tilemap.SetTile(vec, Manager.Resources.LoadTile("Sprite_Tiles_Soil_9"));
    }

    public TileData(Tilemap tilemap, float water, Vector3Int vec, TileEx parent)
    {
        this.tilemap = tilemap;
        this.vec = vec;
        Water = water;
        this.parent = parent;
    }
    public void GetGrowPoint(float point)
    {
        GrowPoint = point;
    }
}


