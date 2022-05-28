using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public Field

    public static GameManager instance;

    [ReadOnly]
    public UIManager uiManager;

    [ReadOnly]
    public PlayerSkillManager skillManager;        

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {        
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        #region Caching

        uiManager = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<UIManager>();

        skillManager = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<PlayerSkillManager>();

        #endregion
    }

    #endregion
}
