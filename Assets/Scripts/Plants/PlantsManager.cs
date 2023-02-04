using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsManager : MonoBehaviour
{
    static public PlantsManager instance;

    PlantBehavior[] plants;
    public List<PlantBehavior> plantsList = new List<PlantBehavior>();

    void Awake()
    {
        instance = this;
    }

    private void Start() 
    {
        plants = FindObjectsOfType<PlantBehavior>();
        foreach (var plant in plants)
        {
            plantsList.Add(plant);
        }
    }

    public void FertilizePlant(FertilizerType fertilizerType)
    {
        PlantBehavior  plant;

        plant = plantsList.Find(x => !x.fertilized && x.fertilizerNeeded == fertilizerType);
        if (!plant)
            return ;
        plant.FertilizePlant();
    }

    public void SwitchHeatPlant(int plantNumber)
    {
        PlantBehavior  plant;

        plant = plantsList.Find(x => x.plantNumber == plantNumber);
        if (!plant)
            return ;
        plant.HeatSwitch();
    }

}
