using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FertilizerVaseBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                                    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float  timeToFill;
    [SerializeField] private Sprite emptyVase;
    [SerializeField] private Sprite basicVase;
    [SerializeField] private Sprite rocksVase;
    [SerializeField] private Sprite rootingVase;
    [SerializeField] HealthBar progressBar;


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
        calcTimeToFill = timeToFill;
        idlePosition = transform.position;
        topping = FertilizerType.None;
        progressBar.gameObject.SetActive(false);
    }

    private void    Update() 
    {
        if (filling)
            UpdateFillVase();
    }

    private void FertilizePlant()
	{
        if (!PlantsManager.instance.FertilizePlant(topping))
            return ;
        filled = false;
        topping = FertilizerType.None;
		GetComponent<SpriteRenderer>().sprite = emptyVase;
	}

    public void AddTopping(FertilizerType fertilizerType)
    {
        topping = fertilizerType;
        if (fertilizerType == FertilizerType.Rooting)
            GetComponent<SpriteRenderer>().sprite = rootingVase;
        else if (fertilizerType == FertilizerType.Rocks)
            GetComponent<SpriteRenderer>().sprite = rocksVase;
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
        progressBar.gameObject.SetActive(true);
        progressBar.SetSize(0);
	}

    private void    UpdateFillVase()
    {
        timeFilling += Time.deltaTime;
        progressBar.SetSize(timeFilling / calcTimeToFill);
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
        GetComponent<SpriteRenderer>().sprite = basicVase;
        progressBar.gameObject.SetActive(false);
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
