using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftJoyStick : JoyStick
{
    public GameObject playerMoveDirObject;

    private void Update()
    {
        playerMoveDirObject.transform.localPosition = lever.localPosition.normalized;
    }
}
