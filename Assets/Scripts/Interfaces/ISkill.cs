using System.Collections;
using System.Collections.Generic;

public interface ISkill
{
    bool IsSkillReady { get; }      // 스킬 준비 변수

    bool IsSkillFinish { get; }

    //------------------------------------------------------------------------------------------------

    PlayerSkillData GetPlayerSkillData();

    void ReinforceSkill();

    void ActivateSkill();    //  스킬 활성화

    IEnumerator WaitSkillDelay();   //  스킬 딜레이만큼 기다린 후 스킬을 준비 상태로 전환
}
