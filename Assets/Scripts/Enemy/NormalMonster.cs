using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class NormalMonster : Monster, IDamageable
{
    [Tooltip("몬스터 공격 딜레이(초)")]
    public float attackDelay = 1.0f;

    [SerializeField, ReadOnly]
    bool isAttackReady = true;

    private void Awake()
    {                                
        monsterRigidbody = GetComponent<Rigidbody2D>();
        targetObject = GameObject.FindWithTag("Player");                
    }

    private void Start()
    {
        moveSpeed = 1f;
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (isAttackReady)
            {
                Attack(collision.gameObject.GetComponent<IDamageable>());
                isAttackReady = false;
                StartCoroutine(AttackDelayCycle());
            }
        }                                
    }
    
    #region ActionMethod
    protected override void Attack(IDamageable target)
    {        
        target.TakeDamage(currentAttackPower);        
    }    

    protected override void Move()
    {
        monsterRigidbody.velocity = (targetObject.transform.position - transform.position).normalized * moveSpeed;        
    }
    protected override void Die()
    {
        gameObject.SetActive(false);
    }
    #endregion

    public void TakeDamage(float damageValue)
    {
        CurrentHp -= damageValue;
    }

    IEnumerator AttackDelayCycle()
    {
        float t = 0;

        while (t <= attackDelay)
        {
            t += Time.deltaTime;
            yield return null;
        }

        isAttackReady = true;
    }


}
