using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster : MonoBehaviour
{
    #region Public Field

    public virtual float CurrentHp
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

    public float MaxHp;

    #endregion

    #region Protected Field

    [SerializeField, ReadOnly]
    protected float currentHp;

    [SerializeField]
    protected Rigidbody2D monsterRigidbody;

    [SerializeField]
    protected MonsterData monsterData;

    [SerializeField]
    protected Player targetScript;

    protected IDamageable playerIDamageable;

    #endregion

    #region Private Field

    [SerializeField, ReadOnly]
    bool isNormalAttackReady = true;

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

    private void OnEnable()
    {
        isNormalAttackReady = true;

        currentHp = MaxHp;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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

        #region Local Method

        IEnumerator WaitNormalAttackDelay()      //  피격 공격의 쿨타임
        {
            yield return new WaitForSeconds(monsterData.AttackDelay);

            isNormalAttackReady = true;
        }

        #endregion
    }

    protected abstract void Die();    
}
