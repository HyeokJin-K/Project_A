using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossSkill
{
    bool IsSkillReady { get; }      // ��ų �غ� ����

    //------------------------------------------------------------------------------------------------

    void ActivateSkill();    //  ��ų Ȱ��ȭ

    IEnumerator WaitSkillDelay();   //  ��ų �����̸�ŭ ��ٸ� �� ��ų�� �غ� ���·� ��ȯ
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
