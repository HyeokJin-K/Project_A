using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftJoyStick : JoyStick
{
    #region Public Field

    public GameObject playerMoveDirObject;

    #endregion

    //------------------------------------------------------------------------------------------------

    void ResetPosDirObjectPos()
    {
        playerMoveDirObject.transform.localPosition = Vector2.zero;
    }

    void ActiveMoveDirObjectPos()
    {
        playerMoveDirObject.transform.localPosition = lever.localPosition.normalized;
    }

    #region 조이스틱 콜백

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
