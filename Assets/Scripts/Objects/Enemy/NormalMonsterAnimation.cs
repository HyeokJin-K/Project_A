using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterAnimation : MonoBehaviour
{
    #region Public Field

    public List<Sprite> monsterSpriteList = new List<Sprite>();

    public float animationDelay;

    #endregion    

    #region Private Field

    [SerializeField]
    SpriteRenderer monsterSpriteRenderer;

    [SerializeField]
    NormalMonster normalMonster;

    int spriteCount;

    bool isDelayCheck;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        normalMonster = normalMonster == null ? GetComponent<NormalMonster>() : normalMonster;

        monsterSpriteRenderer = monsterSpriteRenderer == null ? GetComponent<SpriteRenderer>() : monsterSpriteRenderer;

        #endregion        
    }

    private void OnEnable()
    {
        isDelayCheck = true;

        spriteCount = 0;
    }

    #endregion

    public void UpdateSprite()
    {
        if (!isDelayCheck)
        {
            return;
        }

        if(normalMonster.MoveDir.x > 0f)
        {
            monsterSpriteRenderer.flipX = true;
        }
        else
        {
            monsterSpriteRenderer.flipX = false;
        }

        monsterSpriteRenderer.sprite = monsterSpriteList[spriteCount];      

        isDelayCheck = false;

        spriteCount++;

        if (spriteCount == monsterSpriteList.Count)
        {
            spriteCount = 0;
        }

        StartCoroutine(WaitAnimateDelay());

        #region Local Method

        IEnumerator WaitAnimateDelay()
        {
            float time = animationDelay;

            while (time >= 0f)
            {
                time -= Time.deltaTime;

                yield return null;
            }

            isDelayCheck = true;
        }

        #endregion
    }

    public bool CheckPosInCamera(Camera mainCamera)
    {
        Vector3 pos = transform.position;

        Vector3 cameraLeftBottomPos = mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, -0.1f, mainCamera.nearClipPlane));

        Vector3 cameraRightTopPos = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, mainCamera.nearClipPlane));

        if (pos.x < cameraLeftBottomPos.x || pos.y < cameraLeftBottomPos.y
            || pos.x > cameraRightTopPos.x || pos.y > cameraRightTopPos.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
