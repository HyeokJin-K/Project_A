using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Public Field
    public RectTransform lever;
    #endregion

    #region Private Field
    Vector3 leverPos;
    float sizeX;
    float sizeXRate;
    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle
    private void Awake()
    {
        #region Caching
        sizeX = gameObject.GetComponent<Image>().sprite.rect.width;
        sizeXRate = gameObject.GetComponent<RectTransform>().rect.width / sizeX;
        #endregion
    }
    #endregion

    #region DragEvent Method
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragMethod();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentLeverPos = eventData.position - (Vector2)gameObject.transform.position;      // 현재 레버의 로컬 좌표
        leverPos = currentLeverPos.magnitude < (sizeX - 5f) * sizeXRate * 0.5f ? currentLeverPos :  //  중심에서 현재 현재 위치까지의 거리 값으로 레버 이동 범위 제한
                                                        currentLeverPos.normalized * (sizeX - 5f) * sizeXRate * 0.5f;
        lever.localPosition = leverPos;

        DragMethod();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.localPosition = Vector2.zero;

        EndDragMethod();
    }
    #endregion

    protected abstract void BeginDragMethod();
    protected abstract void DragMethod();
    protected abstract void EndDragMethod();

}
