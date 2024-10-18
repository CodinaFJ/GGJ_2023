using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform bar;

    void Update()
    {
        if (bar.localScale.y > 0.6)
            SetColor(Color.green);
        else if (bar.localScale.y < 0.6 && bar.localScale.y > 0.333)
            SetColor(Color.yellow);
        else if (bar.localScale.y < 0.333)
            SetColor(Color.red);
    }

    public void SetColor (Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3 (1f, sizeNormalized);
    }


}
