using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region Public Field

    #endregion

    #region Private Field



    #endregion

    //------------------------------------------------------------------------------------------------

    #region Scene Method

    public void LoadGameScene()
    {
        print("LoadGameScene");

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameScene");
    }

    public void LoadMenuScene()
    {
        print("LoadMenuScene");

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MenuScene");

        StopAllCoroutines();
    }

    #endregion
}
