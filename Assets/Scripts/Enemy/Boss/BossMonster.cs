using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster, IDamageable
{
    [SerializeField, ReadOnly]
    List<GameObject> currentSkillList = new List<GameObject>();

    public event Action OnBossIdleState;
    public event Action OnBossMoveState;
    public event Action OnBossAttackState;
    public event Action OnBossPhaseChange;

    int currentPhase = 1; 

    public enum BossState
    {
        Idle,
        Move,
        Attack
    }

    BossState state = BossState.Idle;

    public BossState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            switch (state)
            {
                case BossState.Idle:
                    OnBossIdleState?.Invoke();
                    break;
                case BossState.Move:
                    OnBossMoveState?.Invoke();
                    break;
                case BossState.Attack:
                    OnBossAttackState?.Invoke();
                    break;
            }
        }
    }

    private void Awake()
    {
        SetSkillList(); //  최초 스킬 초기화
        OnBossPhaseChange += SetSkillList;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PhaseChange();
        }
    }

    void Idle()
    {        
    }

    void Move()
    {

    }

    void Attack()
    {

    }

    void PhaseChange()  //  보스 페이즈 변경
    {        
        if(currentPhase < transform.childCount)
        {
            transform.GetChild(currentPhase - 1).gameObject.SetActive(false);
            ++currentPhase;
            transform.GetChild(currentPhase - 1).gameObject.SetActive(true);

            OnBossPhaseChange?.Invoke();
            print("페이즈 변경");
        }
        else
        {
            print("최대 페이즈 입니다");
        }
    }

    void SetSkillList()     // 현재 페이즈에 맞는 스킬로 리스트 초기화
    {
        currentSkillList.Clear();

        Transform phase = transform.GetChild(currentPhase - 1);
        for (int i = 0; i < phase.childCount; i++)
        {
            currentSkillList.Add(phase.GetChild(i).gameObject);
        }
    }

    public void TakeDamage(float damageValue)
    {
        CurrentHp -= damageValue;
    }

    protected override void Die()
    {
        
    }
}
