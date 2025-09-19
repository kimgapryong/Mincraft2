using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    public static Manager Instance {  get { Init(); return _instance; } }

    private ResourcesManager _resources = new ResourcesManager();
    public static ResourcesManager Resources { get { return Instance._resources; } }
    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } } 
    private TileManager _tile = new TileManager();  
    public static TileManager Tile { get { return Instance._tile; } }
    private ItemManager _item = new ItemManager();
    public static ItemManager Item { get { return Instance._item; } }
    private RandomManager _random = new RandomManager();
    public static RandomManager Random { get { return Instance._random; } }

    public static PlayerController Player { get; set; }
    public static InputController Input { get; set; }

    private static void Init()
    {
        if(_instance != null)
            return;

        GameObject go = GameObject.Find("@Manager");
        if(go == null)
        {
            go = new GameObject("@Manager");
            go.AddComponent<Manager>();
        }
        _instance = go.GetComponent<Manager>();
        DontDestroyOnLoad(go);
    }
}
