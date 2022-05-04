using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class NormalMonster : Monster
{        
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

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        monsterRigidbody.velocity = (targetObject.transform.position - transform.position).normalized * moveSpeed;        
    }
}
