using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeaterSwitchBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                                    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField][Range(1,4)]
    public int plantNumber;

    [SerializeField] Sprite buttonOn;
    [SerializeField] Sprite buttonOff;
    

    private bool state = false;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = buttonOff;
    }

    private void Switch()
    {
        if (state)
        {
            state = false;
            spriteRenderer.sprite = buttonOff;
        }
        else
        {
            state = true;
            spriteRenderer.sprite = buttonOn;
        }
        PlantsManager.instance.SwitchHeatPlant(plantNumber);
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		Switch();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//throw new System.NotImplementedException();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//throw new System.NotImplementedException();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//throw new System.NotImplementedException();
	}
}
