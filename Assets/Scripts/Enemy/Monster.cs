using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D monsterRigidbody;

    [SerializeField]
    protected string id;
    [SerializeField]
    protected float maxHp = 5f;
    [SerializeField, ReadOnly]
    float currentHp;
    [SerializeField]
    protected float maxAttackPower = 1f;
    [SerializeField, ReadOnly]
    float currentAttackPower;
    [SerializeField]
    protected float moveSpeed;
    [Tooltip("몬스터 피격 공격 딜레이(초)")]
    protected float attackDelay = 1.0f;
    [SerializeField, ReadOnly]
    bool isNormalAttackReady = true;

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


    [SerializeField]
    protected GameObject targetObject;

    protected abstract void Die();    

    protected void OnEnable()
    {
        CurrentHp = maxHp;
        currentAttackPower = maxAttackPower;        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (isNormalAttackReady)
            {
                NormalAttack(collision.gameObject.GetComponent<IDamageable>());
                isNormalAttackReady = false;
                StartCoroutine(NormalAttackDelayCycle());
            }
        }
    }
    protected void NormalAttack(IDamageable target)     //  피격 공격
    {
        target.TakeDamage(currentAttackPower);
    }

    protected IEnumerator NormalAttackDelayCycle()      //  피격 공격의 쿨타임
    {
        float t = 0;

        while (t <= attackDelay)
        {
            t += Time.deltaTime;
            yield return null;
        }

        isNormalAttackReady = true;
    }
}
