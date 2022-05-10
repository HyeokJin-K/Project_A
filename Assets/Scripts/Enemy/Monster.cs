using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D monsterRigidbody;
    [SerializeField]
    protected MonsterData monsterData;

    [SerializeField, ReadOnly]
    protected float currentHp;
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
    private void Awake()
    {        
    }

    [SerializeField]
    protected GameObject targetObject;
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
    protected void NormalAttack(IDamageable target)     //  피격 공격
    {
        target.TakeDamage(monsterData.AttackPower);
    }
    protected abstract void Die();    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isNormalAttackReady)
            {
                NormalAttack(collision.gameObject.GetComponent<IDamageable>());
                isNormalAttackReady = false;
                StartCoroutine(WaitNormalAttackDelay());
            }
        }
    }

    protected void OnEnable()
    {
        currentHp = monsterData.HealthPoint;
    }


}
