using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    Sunflower,
    AloeVera,
    Cactus
}

[System.Serializable]
public struct   PlantPhase
{
    public int     phaseNumber;
    public Sprite  plantPhaseSprite;
    public float   timeToWater;
    public float   timeToHeat;
    public float   timeToFertilize;
}

public struct   StatsTimes
{
    public float    toWater;
    public float    toHeat;
    public float    toFertilize;
    public float    lastWater;
    public float    lastHeat;
    public float    lastFertilize;
}
