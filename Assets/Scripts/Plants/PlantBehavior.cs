using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                            IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlantInfo plantInfo;
    [SerializeField] private RequesterBehavior waterRequester;
    [SerializeField] private RequesterBehavior heatRequester;
    [SerializeField] private RequesterBehavior fertilizerRequester;
    [SerializeField] private float inicialTimesRandomizer;

    private SpriteRenderer  spriteRenderer;
    private StatsTimes      statsTimes;

    [SerializeField][Range(1,4)]
    public int plantNumber;

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
    }

    private void    FixedUpdate() 
    {

        UpdatePlantStatsControl();
    }

    private void    UpdateTimes()
    {
        statsTimes.toWater = TimeManager.CalculateTimeRandomized(plantInfo.timeToWater);
        statsTimes.toHeat = TimeManager.CalculateTimeRandomized(plantInfo.timeToHeat);
        statsTimes.toCold = TimeManager.CalculateTimeRandomized(plantInfo.timeToCold);
        statsTimes.toFertilize = TimeManager.CalculateTimeRandomized(plantInfo.timeToFertilize);
        statsTimes.lastWater = Random.Range(-inicialTimesRandomizer, 0);
        statsTimes.lastHeatChange = Random.Range(-inicialTimesRandomizer, 0);
        statsTimes.lastFertilize = Random.Range(-inicialTimesRandomizer, 0);
    }

    	private void Die()
	{
		//throw new System.NotImplementedException();
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
            RequestWater();
        }
    }

    private void    UpdateHeatStats()
    {
        statsTimes.lastHeatChange += Time.deltaTime;
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
            fertilizerRequester.SetColor(Color.yellow);
            fertilizerNeeded = FertilizerType.Rooting;
        }
        else if (rand == 2)
        {
            fertilizerRequester.SetColor(Color.red);
            fertilizerNeeded = FertilizerType.Rocks;
        }
        else
        {
            fertilizerRequester.SetColor(Color.black);
            fertilizerNeeded = FertilizerType.None;
        }
    }

    /****************************************************************************************
    ACTIONS
    ****************************************************************************************/
    
    private void    WaterPlant()
    {
        statsTimes.lastWater = 0;
        watered = true;
        waterRequester.SetColor(Color.white);
    }

    private void    RequestWater()
    {
        watered = false;
        statsTimes.toWater = TimeManager.CalculateTimeRandomized(plantInfo.timeToWater);
        waterRequester.SetColor(Color.blue);
    }

    public void    FertilizePlant()
    {
        statsTimes.lastFertilize = 0;
        fertilized = true;
        fertilizerRequester.SetColor(Color.white);
    }

    private void    RequestFertilizer()
    {
        RandomizeFertilizer();
        fertilized = false;
        statsTimes.toFertilize = TimeManager.CalculateTimeRandomized(plantInfo.timeToFertilize);
    }

    public void    HeatSwitch()
    {
        statsTimes.lastHeatChange = 0;
        if (heatOn) heatOn = false;
        else heatOn = true;
        heatOk = true;
        heatRequester.SetColor(Color.white);
    }

    private void    RequestHeat()
    {
        heatOk = false;
        statsTimes.toHeat = TimeManager.CalculateTimeRandomized(plantInfo.timeToHeat);
        heatRequester.SetColor(Color.magenta);
    }

    private void    RequestCold()
    {
        heatOk = false;
        statsTimes.toCold = TimeManager.CalculateTimeRandomized(plantInfo.timeToCold);
        heatRequester.SetColor(Color.cyan);
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
