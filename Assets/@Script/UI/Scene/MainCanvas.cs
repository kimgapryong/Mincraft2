using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : UI_Scene
{
    public List<SlotFragment> _slotList = new List<SlotFragment>();

    public bool slot;
    public Queue<Action> _acionQueue = new Queue<Action>();
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        foreach(SlotFragment fragment in _slotList)
            fragment.SetInfo(this);


        StartCoroutine(WaitRefresh());
        return true;
    }
    
    public void SlotAllRefresh()
    {
        foreach(SlotFragment fragment in _slotList) 
            fragment.Refresh();
    }

    private IEnumerator WaitRefresh()
    {
        while(!slot)
            yield return null;

        while(_acionQueue.Count > 0)
        {
            Action act = _acionQueue.Dequeue();
            act?.Invoke();
        }

    }
}
