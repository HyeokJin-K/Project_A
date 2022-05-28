using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPattern : MonoBehaviour
{
    #region Public Field

    [ReadOnly]
    public bool isSkillReady = true;

    #endregion    

    #region Private Field

    IBossSkill[] bossSkills;

    [SerializeField]
    float skillDelay;    

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        bossSkills = GetComponentsInChildren<IBossSkill>();
    }

    #endregion

    public void DoAttackPattern()
    {
        if (!isSkillReady)
        {
            return;
        }

        for(int i = 0; i < bossSkills.Length; i++)
        {
            bossSkills[i].ActivateSkill();
        }

        isSkillReady = false;

        StartCoroutine(WaitSkillDelay());

        #region Local Method

        IEnumerator WaitSkillDelay()
        {
            yield return new WaitForSeconds(skillDelay);

            isSkillReady = true;
        }

        #endregion
    }    
}
