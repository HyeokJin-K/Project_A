using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    protected string id;
    [SerializeField]
    protected float maxHp = 5f;
    [SerializeField]
    protected float currentHp;
    [SerializeField]
    protected float maxAttackPower = 1f;
    [SerializeField]
    protected float currentAttackPower;
    [SerializeField]
    protected float moveSpeed;

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

    protected Rigidbody2D monsterRigidbody;

    [SerializeField]
    protected GameObject targetObject;

    protected abstract void Move();

    protected abstract void Attack(IDamageable target);

    protected abstract void Die();

    protected void OnEnable()
    {
        CurrentHp = maxHp;
        currentAttackPower = maxAttackPower;        
    }

    public string GetId()
    {
        return id;
    }
}
