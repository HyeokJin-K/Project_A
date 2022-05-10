using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaManager : MonoBehaviour
{
    public static GamaManager instance;

    public enum SceneState
    {
        MainMenu,
        InGame
    }

    [ReadOnly]
    public SceneState sceneState = SceneState.MainMenu;
    
    public float inGameTime;

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

        DontDestroyOnLoad(gameObject);

        StartCoroutine(StartInGameTime());        
    }
    
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        LoadMenuScene();
    //    }

    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        LoadGameScene();
    //    }
    //}

    IEnumerator StartInGameTime()
    {
        while (true)
        {
            yield return new WaitUntil(() => instance.sceneState == SceneState.InGame);
            instance.inGameTime += Time.deltaTime;
        }
    }

    #region SceneMethod
    public IEnumerator LoadGameScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        print("LoadGameScene");
        SceneManager.LoadSceneAsync("GameScene");        
        instance.sceneState = SceneState.InGame;
    }
    public void LoadGameScene()
    {
        print("LoadGameScene");
        SceneManager.LoadSceneAsync("GameScene");
        instance.sceneState = SceneState.InGame;
    }

    public IEnumerator LoadMenuScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        print("LoadMenuScene");
        SceneManager.LoadSceneAsync("MenuScene");
        instance.sceneState = SceneState.MainMenu;
        instance.inGameTime = 0;
    }

    public void LoadMenuScene()
    {
        print("LoadMenuScene");
        SceneManager.LoadSceneAsync("MenuScene");
        instance.sceneState = SceneState.MainMenu;
        instance.inGameTime = 0;
    }

    #endregion
}
