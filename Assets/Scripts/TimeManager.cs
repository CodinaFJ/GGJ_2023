using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    static public float   CalculateTimeRandomized(float time)
    {
        float rand;
        rand = GameManager.instance.GetTimesRandomizer();
        return  (time * Random.Range(1 - rand, 1 + rand)) / GameManager.instance.GetGameSpeed();
    }
}
