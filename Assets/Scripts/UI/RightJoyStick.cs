using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 공격 조이스틱
public class RightJoyStick : JoyStick
{
    public GameObject playerAttackDirObject;

    void MoveDirObjectPos()
    {
        playerAttackDirObject.transform.localPosition = lever.localPosition.normalized * 0.5f;
        playerAttackDirObject.transform.up = (lever.position - transform.position).normalized;
    }

    #region 조이스틱 콜백
    protected override void BeginDragMethod()
    {
        base.BeginDragMethod();
        MoveDirObjectPos();
        playerAttackDirObject.SetActive(true);        
    }

    protected override void DragMethod()
    {
        base.DragMethod();
        MoveDirObjectPos();
    }

    protected override void EndDragMethod()
    {
        base.EndDragMethod();
        playerAttackDirObject.SetActive(false);
    }
    #endregion
}
