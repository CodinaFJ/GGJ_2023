using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    Sunflower,
    AloeVera,
    Cactus
}

public enum FertilizerType
{
    Rooting,
    Rocks,
    None
}

public enum RequestType
{
    Water,
    Cold,
    Heat,
    BasicFert,
    RootingFert,
    RocksFert,
    None
}

[System.Serializable]
public struct   PlantInfo
{
    public float    timeToWater;
    public float    timeToHeat;
    public float    timeToCold;
    public float    timeToFertilize;
    PlantType       plantType;
}

public struct   StatsTimes
{
    public float    toWater;
    public float    toHeat;
    public float    toCold;
    public float    toFertilize;
    public float    lastWater;
    public float    lastHeatChange;
    public float    lastFertilize;
}

[System.Serializable]
public struct RequestSpriteTagged
{
    public Sprite sprite;
    public RequestType requestType;
}
