using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 공격 조이스틱
public class RightJoyStick : JoyStick
{
    public GameObject playerAttackDirObject;

    private void Update()
    {
        playerAttackDirObject.transform.localPosition = lever.localPosition.normalized * 0.5f;

        playerAttackDirObject.transform.up = (lever.position - transform.position).normalized;
    }
}
