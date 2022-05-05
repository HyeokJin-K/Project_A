using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalCycleTimer : MonoBehaviour
{               
    public Dictionary<string, float> skillAttackTimer = new Dictionary<string, float>();
    [SerializeField, ReadOnly]
    List<string> skillList = new List<string>();    //  Ȱ��ȭ �Ǿ� �ִ� ��ų ��� �ν����� â ǥ��    

    private void Awake()
    {
        skillAttackTimer.Add("autoAttack", 0f);    // �⺻ ���� Ÿ�̸� �߰�        
    }

    private void Update()
    {
        skillAttackTimer["autoAttack"] -= Time.deltaTime;        
    }

    public void AddSkillAttackTimeList(string skillName)    // ��ų Ÿ�̸� �߰�
    {
        skillAttackTimer.Add(skillName, 0f);
        skillList.Add(skillName);
        StartCoroutine(StartSkillTimer(skillName));
    }

    public void RemoveSkillAttackTimeList(string skillName)     // ��ų Ÿ�̸� ����
    {
        skillAttackTimer.Remove(skillName);
        skillList.Remove(skillName);
    }

    IEnumerator StartSkillTimer(string skillName)       // �ش� ��ų Ÿ�̸� ����
    {
        while (skillAttackTimer.ContainsKey(skillName))
        {
            skillAttackTimer[skillName] -= Time.deltaTime;
            yield return null;
        }
    }
}
