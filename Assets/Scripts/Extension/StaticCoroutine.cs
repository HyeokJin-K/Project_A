using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour
{
    #region Public Field

    public static StaticCoroutine instance;

    #endregion    

    #region Private Field

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    void Awake()
    {
        if (instance)
        {            
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion    

    public void StartStaticCoroutine(IEnumerator coroutine)
    {        
        StartCoroutine(coroutine);
    }
}
