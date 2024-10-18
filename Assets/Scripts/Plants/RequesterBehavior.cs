using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequesterBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] List<RequestSpriteTagged> requestSprites;
    [SerializeField] HealthBarHori progressBar;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        progressBar.SetSize(0);
        progressBar.gameObject.SetActive(false);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetSprite(RequestType requestType)
    {
        spriteRenderer.sprite = requestSprites.Find(x => x.requestType == requestType).sprite;
    }

    public void SetSize(float size)
    {
        progressBar.SetSize(size);
    }

    public void SetBar(bool state)
    {
        progressBar.gameObject.SetActive(state);
    }
}
