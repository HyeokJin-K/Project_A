using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster : MonoBehaviour
{
    #region Public Field

    public float CurrentHp
    {
        get
        {
            return currentHp;
        }
        set
        {
            currentHp = value;
            if(currentHp <= 0)
            {
                Die();
            }
        }
    }

    #endregion

    #region Private Field

    [SerializeField, ReadOnly]
    bool isNormalAttackReady = true;

    #endregion

    #region Protected Field

    [SerializeField]
    protected Rigidbody2D monsterRigidbody;

    [SerializeField]
    protected MonsterData monsterData;

    [SerializeField, ReadOnly]
    protected float currentHp;

    [SerializeField]
    protected GameObject targetObject;

    protected IDamageable playerIDamageable;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIDamageable = collision.gameObject.GetComponent<IDamageable>();

            StartCoroutine(NormalAttack());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIDamageable = null;
        }
    }    

    #endregion

    IEnumerator NormalAttack()     //  피격 공격
    {
        while(playerIDamageable != null)
        {
            if (isNormalAttackReady)
            {
                playerIDamageable.TakeDamage(monsterData.AttackPower);

                isNormalAttackReady = false;

                StartCoroutine(WaitNormalAttackDelay());
            }

            yield return null;
        }
    }

    protected IEnumerator WaitNormalAttackDelay()      //  피격 공격의 쿨타임
    {
        float t = 0;

        while (t <= monsterData.AttackDelay)
        {
            t += Time.deltaTime;

            yield return null;
        }

        isNormalAttackReady = true;
    }

    protected abstract void Die();    
}
