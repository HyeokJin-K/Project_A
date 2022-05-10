using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ���̽�ƽ
public class RightJoyStick : JoyStick
{
    public GameObject playerAttackDirObject;    
    IWeapon weapon;

    void ActiveAttackDirObjectPos()
    {
        playerAttackDirObject.transform.localPosition = lever.localPosition.normalized * 0.5f;
        playerAttackDirObject.transform.up = (lever.position - transform.position).normalized;
    }

    #region ���̽�ƽ �ݹ�
    protected override void BeginDragMethod()
    {
        ActiveAttackDirObjectPos();

        weapon = weapon == null ? playerAttackDirObject.GetComponentInChildren<IWeapon>() : weapon;
        weapon.SetWeaponInput();
    }

    protected override void DragMethod()
    {
        ActiveAttackDirObjectPos();        
    }

    protected override void EndDragMethod()
    {
        weapon.SetWeaponInput();
    }
    #endregion
}
