using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossSkill
{
    bool IsSkillReady { get; }      // 스킬 준비 변수

    //------------------------------------------------------------------------------------------------

    void ActivateSkill();    //  스킬 활성화

    IEnumerator WaitSkillDelay();   //  스킬 딜레이만큼 기다린 후 스킬을 준비 상태로 전환
    /*{
        float t = skillDelay;

        while (t >= 0f)
        {
            t -= Time.deltaTime;

            yield return null;
        }

        isSkillReady = true;
    }*/
}
