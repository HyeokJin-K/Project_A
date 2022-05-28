using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossMonster : Monster, IDamageable, IMoveable
{
    public enum BossState
    {
        Idle,
        Move,
        Attack
    }

    #region Event

    public event Action OnBossMove;

    public event Action OnBossMoveStop;

    public event Action OnBossPhaseChange;

    Action OnBossStateChange;

    #endregion

    #region Public Field        

    public BossState State
    {
        get => state;
        private set
        {
            state = value;

            OnBossStateChange?.Invoke();
        }
    }

    public override float CurrentHp 
    { 
        get => base.CurrentHp;
        set 
        { 
            base.CurrentHp = value;

            hpUI.fillAmount = currentHp / MaxHp;
        }
    }

    public Vector3 MoveDir
    {
        get
        {
            return moveDir;
        }
        private set
        {
            moveDir = value;

            if(moveDir.sqrMagnitude != 0f)
            {
                OnBossMove?.Invoke();
            }
            else
            {
                OnBossMoveStop?.Invoke();
            }
        }
    }

    #endregion

    #region Private Field

    [SerializeField, ReadOnly]
    BossState state = BossState.Idle;

    [SerializeField]
    Image hpUI;

    [SerializeField, ReadOnly]
    List<GameObject> currentSkillList = new List<GameObject>();

    [SerializeField]
    float minWarpDistance;

    [SerializeField]
    GameObject phaseObject;

    int currentPhase = 1;

    Dictionary<string, List<GameObject>> stateSkills = new Dictionary<string, List<GameObject>>();

    Vector3 moveDir;    

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        targetScript = GameObject.FindWithTag("Player").GetComponent<Player>();

        phaseObject = phaseObject == null ? transform.Find("PhaseList").gameObject : phaseObject;

        #endregion

        SetSkillList(); //  최초 스킬 초기화

        OnBossPhaseChange += SetSkillList;

        OnBossStateChange += CheckState;
        
        foreach(var state in Enum.GetValues(typeof(BossState)))
        {            
            stateSkills.Add(state.ToString(), new List<GameObject>());
        }

        State = BossState.Idle;
    }

    #endregion

    #region Boss FSM

    void Idle()
    {
        State = BossState.Move;

        #region Local Method
        

        #endregion
    }

    public void Move()
    {
        StartCoroutine(DoMove());

        #region Local Method

        IEnumerator DoMove()
        {
            float doMoveDurationTime = Random.Range(5f, 8f);

            while(state == BossState.Move)
            {
                if (targetScript == null)
                {
                    break;
                }                

                if(doMoveDurationTime <= 0f)
                {
                    break;
                }

                MoveDir = (targetScript.transform.position - transform.position).normalized;

                monsterRigidbody.velocity = MoveDir * monsterData.MoveSpeed;

                Warp(true);

                doMoveDurationTime -= Time.deltaTime;

                yield return null;
            }

            MoveDir = Vector3.zero;

            monsterRigidbody.velocity = Vector3.zero;

            State = BossState.Attack;
        }

        #endregion
    }

    void Attack()
    {
        StartCoroutine(DoSkillAtack());

        #region Local Method

        IEnumerator DoSkillAtack()
        {
            List<BossAttackPattern> patternList = new List<BossAttackPattern>();

            foreach(var skill in currentSkillList)
            {
                BossAttackPattern pattern = skill.GetComponent<BossAttackPattern>();

                if (pattern.isSkillReady)
                {
                    patternList.Add(pattern);
                }                
            }

            int a = Random.Range(0, patternList.Count);

            patternList[a].DoAttackPattern();

            print(patternList[a].name + " 실행됨");

            GetComponent<BossAnimation>().TurnOnMissileAttackTrigger();

            yield return new WaitForSeconds(1.0f);

            State = BossState.Idle;
        }

        #endregion
    }

    #endregion    

    protected override void Die()
    {

    }

    void CheckState()
    {
        switch (state)
        {
            case BossState.Idle:
                Idle();

                break;

            case BossState.Move:
                Move();

                break;

            case BossState.Attack:
                Attack();

                break;
        }        
    }

    void PhaseChange()  //  보스 페이즈 변경
    {        
        if (currentPhase < transform.childCount)
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

        Transform phase = phaseObject.transform.GetChild(currentPhase - 1);

        for (int i = 0; i < phase.childCount; i++)
        {
            currentSkillList.Add(phase.GetChild(i).gameObject);
        }
    }    

    public void Warp(bool isCheckDistance)
    {
        float randomAngleValue = Random.Range(0f, 360f) * Mathf.Rad2Deg;        

        Vector3 randomVector = new Vector3(Mathf.Cos(randomAngleValue), Mathf.Sin(randomAngleValue));        

        if (isCheckDistance)
        {
            if((targetScript.transform.position - transform.position).sqrMagnitude >= minWarpDistance * minWarpDistance)
            {
                transform.position = targetScript.transform.position + randomVector;
            }
        }
        else
        {
            transform.position = targetScript.transform.position + randomVector;            
        }
    }

    public void TakeDamage(float damageValue)
    {
        CurrentHp -= damageValue;
    }

    public Vector3 GetMoveDir()
    {
        return moveDir;
    }
}
