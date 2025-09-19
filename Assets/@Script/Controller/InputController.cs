using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class InputController : BaseController
{
    public Vector3Int curVec;
    public Tilemap solid;
    public TileData curTile;
    public Define.Item curItem;
    private GameObject marker;
    public GameObject worldSeed;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Manager.Input = this;
        marker = GameObject.Find("Marker");
        return true;
    }
    private void Update()
    {
        if(EventSystem.current && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            Vector3Int cur = solid.WorldToCell(worldPos);

            if(solid.GetTile(cur) == null)
                return;

            
            if(curVec != cur)
            {
                curVec = cur;
                GetCurTile();
            }
            else
            {
                if(curItem == Define.Item.None)
                {
                    if(worldSeed != null)
                        Destroy(worldSeed);
                    worldSeed = Manager.Resources.Instantiate("WorldSeed", worldPos, Quaternion.identity); 
                    
                }
                else
                    Manager.Item._itemActionDic[curItem]?.Invoke();
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(curTile == null)
                return ;

            Manager.UI.CloseAllPopUI();
            Manager.UI.ShowPopUI<GroundPop>(callback: (pop) =>
            {
                pop.SetInfo(curTile);
            });
        }

        if (worldSeed != null)
            if (Vector2.Distance(Manager.Player.transform.position, worldSeed.transform.position) >= 4f)
                Destroy(worldSeed);
    }
    private void GetCurTile()
    {
        curTile = Manager.Tile.GetTileData(curVec);
        
        if(curTile == null)
            return;

        if(solid.GetTile(curVec) == null || curTile.parent.Lock == false)
            return;

        marker.transform.position = curVec + Vector3.one * 0.5f;
    }
}
