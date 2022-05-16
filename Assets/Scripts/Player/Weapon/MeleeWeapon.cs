using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : MonoBehaviour
{
    #region Public Field

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

    #endregion

    #region Protected Field

    [SerializeField]
    protected float attackPower;

    [SerializeField]
    protected float attackDelay;

    [SerializeField]
    protected float attackRange;

    [SerializeField, ReadOnly]
    protected bool isNormalAttackReady = true;

    [SerializeField, ReadOnly]
    protected List<Collider2D> colEnemyList = new List<Collider2D>();

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        isNormalAttackReady = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)   //  무기 범위 안에 들어온 몬스터들을 리스트에 추가
    {
        if (collision.CompareTag("Monster") || collision.CompareTag("BossMonster"))
        {
            if (!colEnemyList.Contains(collision))
            {
                colEnemyList.Add(collision);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)  // 범위 안에 있는 몬스터들을 공격
    {
        if (collision.CompareTag("Monster") || collision.CompareTag("BossMonster"))
        {
            WeaponNormalAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //  범위 밖으로 벗어난 몬스터를 리스트에서 제외
    {
        if (colEnemyList.Contains(collision))
        {
            colEnemyList.Remove(collision);
        }
    }

    #endregion

    protected abstract void WeaponNormalAttack();      // 일반 몬스터 공격

    protected IEnumerator WaitNormalAttackDelay()
    {
        float temp = attackDelay;

        while (temp >= 0f)
        {
            temp -= Time.deltaTime;            

            yield return null;
        }

        isNormalAttackReady = true;
    }

}
