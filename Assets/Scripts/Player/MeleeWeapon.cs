using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public EdgeCollider2D weaponCol;
    [Tooltip("PlayerGlobalCycleTimer 스크립트")]
    public PlayerGlobalCycleTimer cycleTimer;

    [SerializeField]
    protected float attackPower;
    [SerializeField]
    protected float attackDelay;
    [SerializeField]
    protected float attackRange;

    //[SerializeField, ReadOnly]
    //protected bool isAttackReady = false;

    [SerializeField, ReadOnly]
    protected List<Collider2D> colEnemyList = new List<Collider2D>();
    [SerializeField, ReadOnly]
    bool isEnemyCol = false;

    Action OnEnemyCol;

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

    public void Attack()
    {
        if (cycleTimer.skillAttackTimer["autoAttack"] < 0f)
        {
            try
            {
                foreach (var enemy in colEnemyList)
                {
                    enemy.GetComponent<IDamageable>().TakeDamage(attackPower);
                    print(enemy.name+"공격");
                }
            }
            catch (InvalidOperationException) { }
            cycleTimer.skillAttackTimer["autoAttack"] = attackDelay;
        }       
    }

    IEnumerator CorAttack()
    {
        while(colEnemyList.Count != 0)
        {            
            Attack();
            yield return null;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Monster") || collision.tag.Equals("BossMonster"))
        {
            if (!colEnemyList.Contains(collision))
            {
                StopAllCoroutines();
                colEnemyList.Add(collision);
                StartCoroutine(CorAttack());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colEnemyList.Contains(collision))
        {
            colEnemyList.Remove(collision);
        }
    }

}
