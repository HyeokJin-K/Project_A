using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    [Tooltip("PlayerGlobalCycleTimer ��ũ��Ʈ")]
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

    public void Attack()        // �Ϲ� ���� ����
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

    protected void OnTriggerEnter2D(Collider2D collision)   //  ���� ���� �ȿ� ���� ���͵��� ����Ʈ�� �߰�
    {
        if(collision.tag.Equals("Monster") || collision.tag.Equals("BossMonster"))
        {
            if (!colEnemyList.Contains(collision))
            {                
                colEnemyList.Add(collision);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)  // ���� �ȿ� �ִ� ���͵��� ����
    {
        if (collision.tag.Equals("Monster") || collision.tag.Equals("BossMonster"))
        {
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //  ���� ������ ��� ���͸� ����Ʈ���� ����
    {
        if (colEnemyList.Contains(collision))
        {
            colEnemyList.Remove(collision);
        }
    }

}
