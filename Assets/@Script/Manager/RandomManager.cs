using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    public bool RollPercent(float percent)
    {
        if (percent >= 100) return true;
        if(percent < 0) return false;
        return Random.value < (percent / 100);  

    }

    public int GetRandomValue(float[] percent)
    {
        float random = Random.value * 100;
        float per = 0f;

        for(int i = 0; i < percent.Length; i++)
        {
            per += percent[i];
            if(random < per)
                return i;
        }

        return 0;
    }
}
