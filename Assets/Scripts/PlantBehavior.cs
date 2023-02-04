using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                            IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float  dryTime;

    private float   timeFromLastWater = 0;
    private float   calculatedDryTime;
    private bool    dry = false;

    private void Start() {
        calculatedDryTime = CalculateDryTime();
    }

    private void    Update() 
    {
        DryPlantControl();
    }

    private void    DryPlantControl()
    {
        calculatedDryTime = CalculateDryTime();//TODO: Remove this line
        timeFromLastWater += Time.deltaTime;
        if (timeFromLastWater > calculatedDryTime && !dry)
            DryPlant();
    }

    private void    WaterPlant()
    {
        timeFromLastWater = 0;
        dry = false;
        GetComponent<SpriteRenderer>().color = Color.green;
        //TODO: Proper reaction to water
    }

    private void    DryPlant()
    {
        dry = true;
        GetComponent<SpriteRenderer>().color = Color.gray;
        //TODO: Proper reaction to dry
    }

    private float   CalculateDryTime()
    {
        float rand;
        rand = GameManager.instance.GetDryRandomizer();
        return  (dryTime * Random.Range(1 - rand, 1 + rand)) / GameManager.instance.GetGameSpeed();
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
