using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 20;
    Stack<UI_Pop> _popStack = new Stack<UI_Pop>();

    public UI_Scene SceneUI {  get; private set; }
    public GameObject Root
    {
        get
        {
            GameObject go = GameObject.Find("@UI_Root");
            if (go == null)
                go = new GameObject("@UI_Root");
            return go;
        }
    }

    public void SetCanvas(GameObject obj, bool sort = true)
    {
        Canvas canvas = obj.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public void MakeSubItem<T>(Transform pos = null, string key = null, Action<T> callback =null) where T : UI_Base
    {
        if(string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Fragment/{key}", pos, (go) =>
        {
            T subItem = go.GetOrAddComponent<T>();
            callback?.Invoke(subItem);
        });
    }
    public void ShowSceneUI<T>(string key = null, Action<T> callback = null) where T : UI_Scene
    {
        if(string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Scene/{key}", Root.transform, (go) =>
        {
            T scene = go.GetOrAddComponent<T>();
            SceneUI = scene;
            callback?.Invoke(scene);
        });
    }

    public void ShowPopUI<T>(Transform parent = null, string key = null, Action<T> callback = null) where T : UI_Pop
    {
        if(string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Pop/{key}", null, (go) =>
        {
            T pop = go.GetOrAddComponent<T>();
            _popStack.Push(pop);
            if (parent != null)
                go.transform.SetParent(parent);
            else
                go.transform.SetParent(Root.transform);

            callback?.Invoke(pop);
        });
    }

    public void ClosePopUI(UI_Pop pop)
    {
        if(_popStack.Peek() != pop || _popStack.Count == 0)
            return;

        ClosePopUI();
    }

    public void ClosePopUI()
    {
        if (_popStack.Count == 0)
            return;

        UI_Pop pop = _popStack.Pop();
        UnityEngine.Object.Destroy(pop.gameObject);
        pop = null;
        _order--;
    }
    public void CloseAllPopUI()
    {
        while(_popStack.Count > 0)
            ClosePopUI();
    }
}
