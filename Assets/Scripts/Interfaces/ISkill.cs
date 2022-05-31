using System.Collections;
using System.Collections.Generic;

public interface ISkill
{
    bool IsSkillReady { get; }      // ��ų �غ� ����

    bool IsSkillFinish { get; }

    //------------------------------------------------------------------------------------------------

    PlayerSkillData GetPlayerSkillData();

    void ReinforceSkill();

    void ActivateSkill();    //  ��ų Ȱ��ȭ

    IEnumerator WaitSkillDelay();   //  ��ų �����̸�ŭ ��ٸ� �� ��ų�� �غ� ���·� ��ȯ
}
