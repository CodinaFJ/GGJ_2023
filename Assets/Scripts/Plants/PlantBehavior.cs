using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                            IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlantType          plantType;
    [SerializeField] private List<PlantPhase>   plantPhasesList;

    private bool            watered = false;
    private bool            heated = false;
    private bool            fertilized = false;
    private PlantPhase      activePhase;
    private SpriteRenderer  spriteRenderer;
    private StatsTimes      statsTimes;

    private void Start() 
    {
        activePhase = plantPhasesList.Find(x => x.phaseNumber == 1);
        UpdateTimes();
    }

    private void    Update() 
    {
        UpdatePlantStatsControl();
    }

	private float   CalculateTimeRandomized(float time)
    {
        float rand;
        rand = GameManager.instance.GetDryRandomizer();
        return  (time * Random.Range(1 - rand, 1 + rand)) / GameManager.instance.GetGameSpeed();
    }

    private void    UpdateTimes()
    {
        statsTimes.toWater = CalculateTimeRandomized(activePhase.timeToWater);
        statsTimes.toHeat = CalculateTimeRandomized(activePhase.timeToHeat);
        statsTimes.toFertilize = CalculateTimeRandomized(activePhase.timeToFertilize);
    }

    private void   Grow()
    {
        activePhase = plantPhasesList.Find(x => x.phaseNumber == activePhase.phaseNumber + 1);
        spriteRenderer.sprite = activePhase.plantPhaseSprite;
    }

    	private void Die()
	{
		throw new System.NotImplementedException();
	}

    /****************************************************************************************
    UPDATE STATS
    ****************************************************************************************/

    private void    UpdatePlantStatsControl()
    {
        UpdateWaterStats();
        UpdateHeatStats();
        UpdateFetilizerStats();
    }

    private void    UpdateWaterStats()
    {
        statsTimes.lastWater += Time.deltaTime;
        if (!watered && statsTimes.lastWater > statsTimes.toWater * 2)
        {
            Die();
        }
        else if (watered && statsTimes.lastWater > statsTimes.toWater)
        {
            DryPlant();
        }
    }

    private void    UpdateHeatStats()
    {
        statsTimes.lastHeat += Time.deltaTime;
        if (!heated && statsTimes.lastHeat > statsTimes.toHeat * 2)
        {
            Die();
        }
        else if (heated && statsTimes.lastHeat > statsTimes.toHeat)
        {
            throw new System.NotImplementedException();
        }
    }

    private void    UpdateFetilizerStats()
    {
        statsTimes.lastFertilize += Time.deltaTime;
        if (!fertilized && statsTimes.lastFertilize > statsTimes.toFertilize * 2)
        {
            Die();
        }
        else if (fertilized && statsTimes.lastFertilize > statsTimes.toFertilize)
        {
            throw new System.NotImplementedException();
        }
    }

    /****************************************************************************************
    ACTIONS
    ****************************************************************************************/
    
    private void    WaterPlant()
    {
        statsTimes.lastWater = 0;
        watered = true;
        spriteRenderer.color = Color.green;
        //TODO: Proper reaction to water
    }

    private void    DryPlant()
    {
        watered = false;
        statsTimes.toWater = CalculateTimeRandomized(activePhase.timeToWater);
        spriteRenderer.color = Color.gray;
        //TODO: Proper reaction to dry
    }

    private void    FertilizePlant()
    {
        //TODO: Implement
    }

    private void    HeatPlant()
    {
        //TODO: Implement
    }

    /****************************************************************************************
    EVENT SYSTEMS
    ****************************************************************************************/
	public void OnPointerDown(PointerEventData eventData)
	{
        WaterPlant();
	}
    
	public void OnPointerUp(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        //TODO: JUICE
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        //TODO: JUICE
	}
}
