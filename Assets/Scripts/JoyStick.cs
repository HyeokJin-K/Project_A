using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform lever;

    Vector3 leverPos;
    float sizeX;
    float sizeXRate;   

    private void Start()
    {
        sizeX = gameObject.GetComponent<Image>().sprite.rect.width;
        sizeXRate = gameObject.GetComponent<RectTransform>().rect.width / sizeX;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin");
    }

    public void OnDrag(PointerEventData eventData)
    {        
        Vector2 currentLeverPos = eventData.position - (Vector2)gameObject.transform.position;
        leverPos = currentLeverPos.magnitude < (sizeX - 5f) * sizeXRate * 0.5f ? currentLeverPos :
                                                        currentLeverPos.normalized * (sizeX - 5f) * sizeXRate * 0.5f;
        lever.localPosition = leverPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.localPosition = Vector2.zero;        
        print("end");
    }

}
