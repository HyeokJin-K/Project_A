using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    [Tooltip("PlayerGlobalCycleTimer 스크립트")]
    public PlayerGlobalCycleTimer cycleTimer;

    [SerializeField]
    protected float attackPower;
    [SerializeField]
    protected float attackDelay;
    [SerializeField]
    protected float attackRange;

    [SerializeField, ReadOnly]
    protected List<Collider2D> colEnemyList = new List<Collider2D>(); 

    public float AttackPower
    {
        get
        {
            return attackPower;
        }
        set
        {
            attackPower = value;
        }
    }

    public void Attack()        // 일반 몬스터 공격
    {
        if (cycleTimer.skillAttackTimer["autoAttack"] < 0f)
        {
            for (int i = colEnemyList.Count - 1; i >= 0; i--)
            {
                colEnemyList[i].GetComponent<IDamageable>().TakeDamage(attackPower);                               
            }
            cycleTimer.skillAttackTimer["autoAttack"] = attackDelay;
        }
                   
    }

    protected void OnTriggerEnter2D(Collider2D collision)   //  무기 범위 안에 들어온 몬스터들을 리스트에 추가
    {
        if(collision.tag.Equals("Monster") || collision.tag.Equals("BossMonster"))
        {
            if (!colEnemyList.Contains(collision))
            {                
                colEnemyList.Add(collision);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)  // 범위 안에 있는 몬스터들을 공격
    {
        if (collision.tag.Equals("Monster") || collision.tag.Equals("BossMonster"))
        {
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //  범위 밖으로 벗어난 몬스터를 리스트에서 제외
    {
        if (colEnemyList.Contains(collision))
        {
            colEnemyList.Remove(collision);
        }
    }

}
