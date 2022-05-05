using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalCycleTimer : MonoBehaviour
{               
    public Dictionary<string, float> skillAttackTimer = new Dictionary<string, float>();
    [SerializeField, ReadOnly]
    List<string> skillList = new List<string>();    //  활성화 되어 있는 스킬 목록 인스펙터 창 표시    

    private void Awake()
    {
        skillAttackTimer.Add("autoAttack", 0f);    // 기본 공격 타이머 추가        
    }

    private void Update()
    {
        skillAttackTimer["autoAttack"] -= Time.deltaTime;        
    }

    public void AddSkillAttackTimeList(string skillName)    // 스킬 타이머 추가
    {
        skillAttackTimer.Add(skillName, 0f);
        skillList.Add(skillName);
        StartCoroutine(StartSkillTimer(skillName));
    }

    public void RemoveSkillAttackTimeList(string skillName)     // 스킬 타이머 제거
    {
        skillAttackTimer.Remove(skillName);
        skillList.Remove(skillName);
    }

    IEnumerator StartSkillTimer(string skillName)       // 해당 스킬 타이머 시작
    {
        while (skillAttackTimer.ContainsKey(skillName))
        {
            skillAttackTimer[skillName] -= Time.deltaTime;
            yield return null;
        }
    }
}
