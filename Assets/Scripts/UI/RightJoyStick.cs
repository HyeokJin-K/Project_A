using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ���̽�ƽ
public class RightJoyStick : JoyStick
{
    public GameObject playerAttackDirObject;

    void ResetPosDirObjectPos()
    {
        playerAttackDirObject.transform.localPosition = Vector2.zero;
        playerAttackDirObject.transform.up = Vector2.zero;
    }

    void MoveDirObjectPos()
    {
        playerAttackDirObject.transform.localPosition = lever.localPosition.normalized * 0.5f;
        playerAttackDirObject.transform.up = (lever.position - transform.position).normalized;
    }

    void OnOffWeaponEnableCheck()
    {        
        //playerAttackDirObject.transform.GetComponentInChildren<IWeapon>().OnOffWeaponAttackReady();        
    }

    #region ���̽�ƽ �ݹ�
    protected override void BeginDragMethod()
    {
        base.BeginDragMethod();
        MoveDirObjectPos();
        playerAttackDirObject.SetActive(true);
        //OnOffWeaponEnableCheck();
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
        playerAttackDirObject.SetActive(false);
        //OnOffWeaponEnableCheck();
    }
    #endregion
}
