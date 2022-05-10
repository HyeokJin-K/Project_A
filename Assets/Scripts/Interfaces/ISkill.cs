using System.Collections;
using System.Collections.Generic;


public interface ISkill
{
    float SkillPower { get; set; }
    float SkillDelay { get; set; }      // ��ų ������
    bool IsSkillReady { get; }      // ��ų �غ� ����

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
