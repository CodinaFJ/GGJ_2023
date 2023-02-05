using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequesterBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] List<RequestSpriteTagged> requestSprites;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetSprite(RequestType requestType)
    {
        spriteRenderer.sprite = requestSprites.Find(x => x.requestType == requestType).sprite;
    }
}
