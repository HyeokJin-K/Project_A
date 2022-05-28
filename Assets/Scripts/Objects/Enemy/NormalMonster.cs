using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class NormalMonster : Monster, IDamageable, IMoveable
{
    #region Public Field

    public float dropExp;  
    
    public Vector3 MoveDir
    {
        get => moveDir;        
    }

    #endregion

    #region Private Field

    Vector3 moveDir;

    float cameraVertexMagnitude;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        monsterRigidbody = GetComponent<Rigidbody2D>();

        targetScript = GameObject.FindWithTag("Player").GetComponent<Player>();

        #endregion        

        cameraVertexMagnitude = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).sqrMagnitude;
    }

    protected void OnEnable()
    {
        currentHp = monsterData.HealthPoint;        
    }

    #endregion

    public void Move()
    {
        moveDir = (targetScript.transform.position - transform.position).normalized;        

        if((targetScript.transform.position - transform.position).sqrMagnitude > cameraVertexMagnitude * 2.5f)
        {
            monsterRigidbody.velocity = moveDir * 10f;
        }
        else
        {
            monsterRigidbody.velocity = moveDir * monsterData.MoveSpeed;    
        }
    }    

    protected override void Die()
    {
        StopAllCoroutines();        

        targetScript.Exp += dropExp;

        gameObject.SetActive(false);
    }

    public void TakeDamage(float damageValue)
    {
        CurrentHp -= damageValue;
    }

    public Vector3 GetMoveDir()
    {
        return MoveDir;
    }
}
