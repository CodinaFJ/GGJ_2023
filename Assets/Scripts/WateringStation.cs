using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringStation : MonoBehaviour
{
    static public WateringStation instance;

    [SerializeField] HealthBar bar;

    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;
    }

    private void Start() {
        spriteRenderer.enabled = false;
        bar.gameObject.SetActive(false);
    }

    public void StartWateringCooldown()
    {
        bar.gameObject.SetActive(true);
        bar.SetSize(0);
        spriteRenderer.enabled = true;
    }

    public void UpdateWateringCooldown(float size)
    {
        bar.SetSize(size);
    }

    public void EndWateringCooldown()
    {
        bar.gameObject.SetActive(false);
        spriteRenderer.enabled = false;
    }
}
