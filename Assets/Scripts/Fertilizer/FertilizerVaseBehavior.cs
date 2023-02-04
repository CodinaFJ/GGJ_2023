using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FertilizerVaseBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                                    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float  timeToFill;

    private float   calcTimeToFill;
    private float   timeFilling = 0;
    private Vector2 idlePosition;

    public Vector2 fillingPosition;
    public bool     filled = false;
    public bool    filling = false;
    public FertilizerType   topping;

    private void    Start() 
    {
        fillingPosition = GameObject.Find("Filler").transform.position;
        calcTimeToFill = TimeManager.CalculateTimeRandomized(timeToFill);
        idlePosition = transform.position;
        topping = FertilizerType.None;
    }

    private void    Update() 
    {
        if (filling)
            UpdateFillVase();
    }

    private void FertilizePlant()
	{
        PlantsManager.instance.FertilizePlant(topping);
        filled = false;
        topping = FertilizerType.None;
		GetComponent<SpriteRenderer>().color = Color.white;
	}

    public void AddTopping(FertilizerType fertilizerType)
    {
        topping = fertilizerType;
        if (fertilizerType == FertilizerType.Rooting)
            GetComponent<SpriteRenderer>().color = Color.yellow;
        else if (fertilizerType == FertilizerType.Rocks)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    /****************************************************************************************
    BASIC FILL VASE
    ****************************************************************************************/

    private void    StartFillVase()
	{
        if (VasesManager.instance.FillerBusy())
            return ;
		filling = true;
        this.gameObject.transform.position = fillingPosition;
        GetComponent<SpriteRenderer>().color = Color.gray;
	}

    private void    UpdateFillVase()
    {
        timeFilling += Time.deltaTime;
        if (timeFilling > calcTimeToFill)
        {
            FinishFillVase();
            timeFilling = 0;
        }
    }

	private void    FinishFillVase()
	{
		filled = true;
        filling = false;
        GetComponent<SpriteRenderer>().color = Color.black;
	}

    private void    ReturnVaseToCounter()
	{
		this.gameObject.transform.position = idlePosition;
	}

	/****************************************************************************************
    EVENT SYSTEMS
    ****************************************************************************************/
	public void OnPointerDown(PointerEventData eventData)
    {
        if (!filled && !filling)
            StartFillVase();
        else if (filled && (Vector2) transform.position == fillingPosition)
            ReturnVaseToCounter();
        else if (filled && (Vector2) transform.position == idlePosition)
            FertilizePlant();
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
