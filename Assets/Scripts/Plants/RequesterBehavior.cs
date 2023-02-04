using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequesterBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
