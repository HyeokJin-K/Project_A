using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class NormalMonster : Monster, IDamageable, IMoveable
{
    #region Public Field

    public float dropExp;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        monsterRigidbody = GetComponent<Rigidbody2D>();

        targetScript = GameObject.FindWithTag("Player").GetComponent<Player>();

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
        monsterRigidbody.velocity = (targetScript.transform.position - transform.position).normalized * monsterData.MoveSpeed;
    }
    protected override void Die()
    {
        targetScript.Exp += dropExp;

        gameObject.SetActive(false);
    }

    #endregion

    public void TakeDamage(float damageValue)
    {
        CurrentHp -= damageValue;
    }

    public Vector3 GetMoveDir()
    {
        return (targetScript.transform.position - transform.position).normalized + transform.position;
    }
}
