using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Public Field    

    public RectTransform lever;

    public Sprite normalLeverSprite;

    public Sprite dirLeverSprite;

    #endregion

    #region Private Field
        
    Image currentLeverImage;

    Vector3 leverPos;

    float sizeX;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        currentLeverImage = lever.GetComponent<Image>();

        sizeX = gameObject.GetComponent<RectTransform>().rect.width;

        #endregion
    }

    #endregion

    #region DragEvent Method

    public void OnPointerDown(PointerEventData eventData)
    {
        currentLeverImage.sprite = dirLeverSprite;

        MoveLever(eventData);

        BeginDragMethod();        
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveLever(eventData);

        DragMethod();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentLeverImage.sprite = normalLeverSprite;

        lever.localPosition = Vector2.zero;

        lever.transform.up = Vector2.up;

        EndDragMethod();
    }

    void MoveLever(PointerEventData eventData)
    {
        Vector2 currentLeverPos = eventData.position - (Vector2)gameObject.transform.position;      // ���� ������ ���� ��ǥ

        leverPos = currentLeverPos.magnitude < sizeX * 0.5f - 5f ? currentLeverPos :  //  �߽ɿ��� ���� ���� ��ġ������ �Ÿ� ������ ���� �̵� ���� ����
                                                        currentLeverPos.normalized * (sizeX * 0.5f - 5f);

        lever.localPosition = leverPos;

        lever.transform.up = currentLeverPos.normalized;
    }

    #endregion

    protected abstract void BeginDragMethod();

    protected abstract void DragMethod();

    protected abstract void EndDragMethod();    
}
