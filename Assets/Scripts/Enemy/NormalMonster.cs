using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class NormalMonster : Monster, IDamageable
{            
    private void Awake()
    {                                
        monsterRigidbody = GetComponent<Rigidbody2D>();
        targetObject = GameObject.FindWithTag("Player");

        moveSpeed = 1f;
    }    
    
    private void FixedUpdate()
    {
        Move();
    }

    #region ActionMethod


    protected void Move()
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

}
