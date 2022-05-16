using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class NormalMonster : Monster, IDamageable, IMoveable
{

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        monsterRigidbody = GetComponent<Rigidbody2D>();

        targetObject = GameObject.FindWithTag("Player");

        #endregion
    }

    protected void OnEnable()
    {
        currentHp = monsterData.HealthPoint;        
    }

    #endregion

    #region MonsterActionMethod

    public void Move()
    {
        monsterRigidbody.velocity = (targetObject.transform.position - transform.position).normalized * monsterData.MoveSpeed;
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

    public Vector3 GetMoveDir()
    {
        return (targetObject.transform.position - transform.position).normalized + transform.position;
    }
}
