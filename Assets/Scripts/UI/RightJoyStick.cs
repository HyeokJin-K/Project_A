using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ���̽�ƽ
public class RightJoyStick : JoyStick
{
    public GameObject playerAttackDirObject;

    void MoveDirObjectPos()
    {
        playerAttackDirObject.transform.localPosition = lever.localPosition.normalized * 0.5f;
        playerAttackDirObject.transform.up = (lever.position - transform.position).normalized;
    }

    #region ���̽�ƽ �ݹ�
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
