using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    #region Public Field

    #endregion    

    #region Private Field

    [SerializeField]
    BossMonster bossMonster;

    [SerializeField]
    Animator bossAnimator;

    [SerializeField]
    SpriteRenderer bossSpriteRenderer;

    bool bossIsMove;

    Vector2 moveDir;

    Vector2 MoveDir
    {
        get
        {
            return moveDir;
        }
        set
        {
            moveDir = value;

            if(moveDir.x < 0)
            {
                bossSpriteRenderer.flipX = true;
            }
            else if(MoveDir.x > 0)
            {
                bossSpriteRenderer.flipX = false;
            }            
        }
    }

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        bossMonster = bossMonster == null ? GetComponent<BossMonster>() : bossMonster;

        bossAnimator = bossAnimator == null ? GetComponent<Animator>() : bossAnimator;

        bossSpriteRenderer = bossSpriteRenderer == null ? GetComponent<SpriteRenderer>() : bossSpriteRenderer;

        #endregion

        bossMonster.OnBossMove += () => MoveDir = bossMonster.MoveDir;

        bossMonster.OnBossMove += () => bossAnimator.SetBool("IsMove", true);

        bossMonster.OnBossMoveStop += () => bossAnimator.SetBool("IsMove", false);
        
    }

    #endregion

    public void TurnOnMissileAttackTrigger()
    {
        bossAnimator.SetTrigger("MissileAttackTrigger");
    }
}
