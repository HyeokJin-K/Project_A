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

    void ActiveMoveDirObjectPos()
    {
        playerMoveDirObject.transform.localPosition = lever.localPosition.normalized;
    }

    #region ���̽�ƽ �ݹ�
    protected override void BeginDragMethod()
    {        
        ActiveMoveDirObjectPos();
    }

    protected override void DragMethod()
    {     
        ActiveMoveDirObjectPos();
    }

    protected override void EndDragMethod()
    {     
        ResetPosDirObjectPos();
    }
    #endregion
}
