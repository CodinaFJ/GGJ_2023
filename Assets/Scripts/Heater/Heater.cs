using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [SerializeField] Sprite spriteOn;
    [SerializeField] Sprite spriteOff;

    SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TurnOff();
    }

    public void TurnOn()
    {
        spriteRenderer.sprite = spriteOn;
    }

    public void TurnOff()
    {
        spriteRenderer.sprite = spriteOff;
    }
}
