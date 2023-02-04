using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToppingBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                            IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] FertilizerType fertilizerType;

	public void OnPointerDown(PointerEventData eventData)
	{
		VasesManager.instance.AddTopping(fertilizerType);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}
}
