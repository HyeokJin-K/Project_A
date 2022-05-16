using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 공격 조이스틱
public class RightJoyStick : JoyStick
{
    #region Public Field

    public GameObject playerAttackDirObject;

    #endregion

    #region Private Field

    IWeapon weapon;

    #endregion

    //------------------------------------------------------------------------------------------------    

    void ActiveAttackDirObjectPos()
    {
        playerAttackDirObject.transform.localPosition = lever.localPosition.normalized * 0.5f;

        playerAttackDirObject.transform.up = (lever.position - transform.position).normalized;
    }

    #region 조이스틱 콜백

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
