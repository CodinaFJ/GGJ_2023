    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarHori : MonoBehaviour
{
    private Transform bar;

    private void Awake() {
        bar = transform.Find("Bar");;
    }

    public void SetColor (Color color){
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3 ( sizeNormalized,1f);
    }

    void Update()
    { 
        if (bar.localScale.x > 0.6)
        {SetColor(Color.green);} 
        else if (bar.localScale.x < 0.6 && bar.localScale.x > 0.333)
        {SetColor(Color.yellow);}
        else if (bar.localScale.x < 0.333)
        {
            SetColor(Color.red);
        }
        
    }
}
