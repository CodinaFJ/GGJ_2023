using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasesManager : MonoBehaviour
{
    public static VasesManager instance;

    FertilizerVaseBehavior[] vases;
    public List<FertilizerVaseBehavior> vasesList = new List<FertilizerVaseBehavior>();

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        vases = FindObjectsOfType<FertilizerVaseBehavior>();
        foreach (var vase in vases)
        {
            vasesList.Add(vase);
        }
    }

    public void AddTopping(FertilizerType fertilizerType)
    {
        FertilizerVaseBehavior  vase;

        vase = vasesList.Find(x => x.filled && x.topping == FertilizerType.None
                                && (Vector2) x.transform.position != x.fillingPosition);
        if (!vase)
            return ;
        vase.AddTopping(fertilizerType);
    }

    public bool FillerBusy()
    {
        if (!vasesList.Find(x => (Vector2) x.transform.position == x.fillingPosition))
            return false;
        return true;
    }


}
