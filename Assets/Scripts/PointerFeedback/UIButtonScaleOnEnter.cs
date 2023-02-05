using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonScaleOnEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    float scaleTweenTime = 0.1f;
    [SerializeField]
    float scaleValue = 1.8f;

    Vector3 initialScale;

    private void Start() {
        initialScale = this.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData){
        try{
            Button myButton  = GetComponent<Button>();
            if(!myButton.interactable) return;
        }catch{}
        LeanTween.cancel(this.gameObject);
        LeanTween.scale(this.gameObject, new Vector3 (Mathf.Sign(this.transform.localScale.x) * scaleValue, Mathf.Sign(this.transform.localScale.y) * scaleValue, 1), scaleTweenTime).setEaseInCirc();
    }

    public void OnPointerExit(PointerEventData eventData){
        LeanTween.cancel(this.gameObject);
            LeanTween.scale(this.gameObject, new Vector3 (Mathf.Sign(this.transform.localScale.x) * Mathf.Abs(initialScale.x), Mathf.Sign(this.transform.localScale.y) * Mathf.Abs(initialScale.y), 1), scaleTweenTime).setEaseOutExpo(); 
    }
}
