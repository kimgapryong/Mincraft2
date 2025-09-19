using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesManager
{
    private Dictionary<string, UnityEngine.Object> _loadDic = new Dictionary<string, UnityEngine.Object>();
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if(_loadDic.TryGetValue(path, out UnityEngine.Object obj))
            return obj as T;

        T objs = Resources.Load<T>($"Prefabs/{path}");
        _loadDic.Add(path, objs);

        return objs;
    }
    public Tile LoadTile(string path)
    {
        return Load<Tile>($"TilePalette/Tiles/{path}");
    }
    public GameObject Instantiate(string path, Vector3 vec, Quaternion quan, Transform parent = null, Action<GameObject> callback = null)
    {
        GameObject obj = Instantiate(path, parent, callback);

        obj.transform.position = vec;
        obj.transform.localRotation = quan;

        return obj;
    }
    public GameObject Instantiate(string path, Transform parent = null, Action<GameObject> callback = null)
    {
        GameObject obj = Load<GameObject>(path);

        GameObject clone = UnityEngine.Object.Instantiate(obj, parent);
        clone.name = obj.name;

        callback?.Invoke(clone);

        return clone;
    }
}
