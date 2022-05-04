using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    protected string id;
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected float attackPower;
    [SerializeField]
    protected float moveSpeed;

    protected Rigidbody2D monsterRigidbody;

    [SerializeField]
    protected GameObject targetObject;

    protected abstract void Move();

    protected abstract void Attack();

    public string GetId()
    {
        return id;
    }
}
