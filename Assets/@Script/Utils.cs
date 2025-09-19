using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Utils 
{
   public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        T com = obj.GetComponent<T>();
        if (com == null) 
            com = obj.AddComponent<T>();

        return com;
    }

    public static T FindChild<T>(this GameObject obj, string name) where T : UnityEngine.Object
    {
        if(typeof(T) == typeof(GameObject))
            return FindObject(obj, name) as T;  

        foreach(var t in obj.transform.GetComponentsInChildren<T>(true))
            if (t.name == name)
                return t;

        return null;
    }
    private static GameObject FindObject(GameObject obj, string name)
    {
        foreach (var t in obj.transform.GetComponentsInChildren<Transform>(true))
            if(t.name == name)
                return t.gameObject;

        return null;
    }
    public static void BindEvent(this GameObject obj, Action callback)
    {
        obj.AddComponent<UI_EventHandler>().clickAction -= callback;
        obj.AddComponent<UI_EventHandler>().clickAction += callback;
    }
}
