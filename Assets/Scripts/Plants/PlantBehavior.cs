using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                            IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlantInfo plantInfo;
    [SerializeField] private float inicialTimesRandomizer;
    [SerializeField][Range(1,4)]
    public int plantNumber;
    [SerializeField] Heater heater;

    private SpriteRenderer  spriteRenderer;
    private StatsTimes      statsTimes;

    [Header("Requesters")]
    [SerializeField] private RequesterBehavior waterRequester;
    [SerializeField] private RequesterBehavior heatRequester;
    [SerializeField] private RequesterBehavior fertilizerRequester;

    [HideInInspector] public bool watered = true;
    [HideInInspector] public bool heatOk = true;
    [HideInInspector] public bool heatOn = false;
    [HideInInspector] public bool fertilized = true;
    [HideInInspector] public  FertilizerType  fertilizerNeeded;

    private void Start() 
    {
        statsTimes = new StatsTimes();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateTimes();
        InitilizeRequesters();
    }

    private void    FixedUpdate() 
    {
        UpdateTimes();
        UpdatePlantStatsControl();
    }

    private void    InitialUpdateTimes()
    {
        UpdateTimes();
        statsTimes.lastWater = Random.Range(-inicialTimesRandomizer, 0);
        statsTimes.lastHeatChange = Random.Range(-inicialTimesRandomizer, 0);
        statsTimes.lastFertilize = Random.Range(-inicialTimesRandomizer, 0);
    }

    private void UpdateTimes()
    {
        statsTimes.toWater = TimeManager.CalculateTimeRandomized(plantInfo.timeToWater);
        statsTimes.toHeat = TimeManager.CalculateTimeRandomized(plantInfo.timeToHeat);
        statsTimes.toCold = TimeManager.CalculateTimeRandomized(plantInfo.timeToCold);
        statsTimes.toFertilize = TimeManager.CalculateTimeRandomized(plantInfo.timeToFertilize);
    }

    private void InitilizeRequesters()
    {
        waterRequester.SetSprite(RequestType.None);
        heatRequester.SetSprite(RequestType.None);
        fertilizerRequester.SetSprite(RequestType.None);
    }

    	private void Die()
	{
        Debug.Log("GAME OVER");
		GameManager.instance.Die();
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
        if (!watered && statsTimes.lastWater < statsTimes.toWater * 2)
            waterRequester.SetSize(1 + ((statsTimes.toWater - statsTimes.lastWater) / (statsTimes.toWater)));
        if (!watered && statsTimes.lastWater > statsTimes.toWater * 2)
        {
            Die();
        }
        else if (watered && statsTimes.lastWater > statsTimes.toWater)
        {
            RequestWater();
        }
    }

    private void    UpdateHeatStats()
    {
        statsTimes.lastHeatChange += Time.deltaTime;
        if (!heatOk && statsTimes.lastHeatChange < statsTimes.toHeat * 2 && heatOn)
            heatRequester.SetSize(1 + ((statsTimes.toHeat - statsTimes.lastHeatChange) / (statsTimes.toHeat)));
        else if (!heatOk && statsTimes.lastHeatChange < statsTimes.toCold * 2 && !heatOn)
            heatRequester.SetSize(1 + ((statsTimes.toCold - statsTimes.lastHeatChange) / (statsTimes.toCold)));
        if (!heatOk && statsTimes.lastHeatChange > statsTimes.toHeat * 2)
        {
            Die();
        }
        else if (heatOk && statsTimes.lastHeatChange > statsTimes.toHeat && !heatOn)
        {
            RequestHeat();
        }
        else if (heatOk && statsTimes.lastHeatChange > statsTimes.toCold && heatOn)
        {
            RequestCold();
        }
    }

    private void    UpdateFetilizerStats()
    {
        statsTimes.lastFertilize += Time.deltaTime;
        if (!fertilized && statsTimes.lastFertilize < statsTimes.toFertilize * 2)
            fertilizerRequester.SetSize(1 + ((statsTimes.toFertilize - statsTimes.lastFertilize) / (statsTimes.toFertilize)));
        if (!fertilized && statsTimes.lastFertilize > statsTimes.toFertilize * 2)
        {
            Die();
        }
        else if (fertilized && statsTimes.lastFertilize > statsTimes.toFertilize)
        {
            RequestFertilizer();
        }
    }

    private void    RandomizeFertilizer()
    {
        int rand = Random.Range(1,4);

        if (rand == 1)
        {
            fertilizerRequester.SetSprite(RequestType.RootingFert);
            fertilizerNeeded = FertilizerType.Rooting;
        }
        else if (rand == 2)
        {
            fertilizerRequester.SetSprite(RequestType.RocksFert);
            fertilizerNeeded = FertilizerType.Rocks;
        }
        else
        {
            fertilizerRequester.SetSprite(RequestType.BasicFert);
            fertilizerNeeded = FertilizerType.None;
        }
    }

    /****************************************************************************************
    ACTIONS
    ****************************************************************************************/
    
    private void    WaterPlant()
    {
        if (watered)
            return ;
        statsTimes.lastWater = 0;
        watered = true;
        waterRequester.SetBar(false);
        //GameManager.instance.UseWaterCan();
        waterRequester.SetSprite(RequestType.None);
        statsTimes.toWater = TimeManager.CalculateTimeRandomized(plantInfo.timeToWater);
    }

    private void    RequestWater()
    {
        watered = false;
        waterRequester.SetSprite(RequestType.Water);
        waterRequester.SetBar(true);
        waterRequester.SetSize(1);
    }

    public void    FertilizePlant()
    {
        if (fertilized)
            return ;
        statsTimes.lastFertilize = 0;
        fertilized = true;
        fertilizerRequester.SetSprite(RequestType.None);
        statsTimes.toFertilize = TimeManager.CalculateTimeRandomized(plantInfo.timeToFertilize);
        fertilizerRequester.SetBar(false);
    }

    private void    RequestFertilizer()
    {
        RandomizeFertilizer();
        fertilized = false;
        fertilizerRequester.SetBar(true);
        fertilizerRequester.SetSize(1);
    }

    public void    HeatSwitch()
    {
        if (!heatOk)
        {
            heatRequester.SetSprite(RequestType.None);
            statsTimes.lastHeatChange = 0;
        }
        if (heatOn)
        {
            statsTimes.toCold = TimeManager.CalculateTimeRandomized(plantInfo.timeToCold);
            heatOn = false;
            heater.TurnOff();
        } 
        else
        {
            statsTimes.toHeat = TimeManager.CalculateTimeRandomized(plantInfo.timeToHeat);
            heatOn = true;
            heater.TurnOn();
        }
        heatOk = true;
        heatRequester.SetBar(false);
    }

    private void    RequestHeat()
    {
        heatOk = false;
        heatRequester.SetSprite(RequestType.Heat);
        heatRequester.SetBar(true);
        heatRequester.SetSize(1);
    }

    private void    RequestCold()
    {
        heatOk = false;
        heatRequester.SetSprite(RequestType.Cold);
        heatRequester.SetBar(true);
        heatRequester.SetSize(1);
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
