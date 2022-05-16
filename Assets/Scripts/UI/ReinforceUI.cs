using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceUI : MonoBehaviour
{
    #region Public Field

    #endregion

    #region Private Field  
    
    [SerializeField]
    GameObject[] optionSlots = new GameObject[3];    

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle    

    private void Awake()
    {        
    }

    #endregion

    public void SetOptionUI(string optionName, Sprite sprite)
    {

    }
}
