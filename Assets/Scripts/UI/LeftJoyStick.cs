using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftJoyStick : JoyStick
{
    public GameObject playerMoveDirObject;
        
    
    void ResetPosDirObjectPos()
    {
        playerMoveDirObject.transform.localPosition = Vector2.zero;
    }

    void MoveDirObjectPos()
    {
        playerMoveDirObject.transform.localPosition = lever.localPosition.normalized;
    }

    #region 조이스틱 콜백
    protected override void BeginDragMethod()
    {
        base.BeginDragMethod();
        MoveDirObjectPos();
    }

    protected override void DragMethod()
    {
        base.DragMethod();
        MoveDirObjectPos();
    }

    protected override void EndDragMethod()
    {
        base.EndDragMethod();
        ResetPosDirObjectPos();
    }
    #endregion
}
