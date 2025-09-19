using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    private Dictionary<Type, UnityEngine.Object[]> _uiDic = new Dictionary<Type, UnityEngine.Object[]>();
    private bool _init;

    private void Start()
    {
        Init();
    }
    protected virtual bool Init()
    {
        if (!_init)
        {
            _init = true;
            return true;
        }

        return false;
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] obj = new UnityEngine.Object[names.Length];
        _uiDic.Add(typeof(T), obj);

        for(int i = 0; i < names.Length; i++)
            obj[i] = gameObject.FindChild<T>(names[i]);
            
    }
    protected void BindObject(Type type) { Bind<GameObject>(type); }
    protected void BindImage(Type type) { Bind<Image>(type); }
    protected void BindTextPro(Type type) { Bind<TextMeshProUGUI>(type); }
    protected void BindText(Type type) { Bind<Text>(type); }
    protected void BindButton(Type type) { Bind<Button>(type); }
    protected void BindSlider(Type type) { Bind<Slider>(type); }
    protected void BindInput(Type type) { Bind<InputField>(type); }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs = null;

        if(_uiDic.TryGetValue(typeof(T), out objs) == false)
            return null;

        return objs[idx] as T;  
    }
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected TextMeshProUGUI GetTextPro(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Slider GetSlider(int idx) { return Get<Slider>(idx); }
    protected InputField GetInput(int idx) { return Get<InputField>(idx); }

}
