using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerGlobalCycleTimer))]
public class Player : MonoBehaviour, IDamageable
{
    public event Action OnPlayerMove;
    public event Action OnPlayerMoveStop;

    public GameObject playerAttackDirObject;
    public GameObject playerMoveDirObject;

    public enum PlayerAction
    {
        Idle,
        Move,
        Attack,
        Dead
    }   

    [SerializeField, Tooltip("체력")]
    float hp;
    public float Hp 
    { 
        get
        {
            return hp;
        }            
        set
        {
            hp = value;
        }        
    }

    [SerializeField, Tooltip("방어력")]
    float defensePower;
    public float DefensePower
    {
        get
        {
            return defensePower;
        }
        set
        {
            defensePower = value;
        }
    }

    [SerializeField, Tooltip("이동속도")]
    float moveSpeed;

    Vector3 moveDir;
    public Vector3 MoveDir 
    {
        get
        {
            return moveDir;
        }
        set
        {
            moveDir = value;
            
            if(moveDir != Vector3.zero)
            {                
                OnPlayerMove?.Invoke();
            }
            else
            {
                OnPlayerMoveStop?.Invoke();
            }
        } 
    }

    [Tooltip("장착 무기 오브젝트")]
    public BoxCollider2D equipWeaponCol;

    public Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();        
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region ActionMethod
    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        MoveDir = (playerMoveDirObject.transform.position - transform.position).normalized;
        playerRigidbody.velocity = moveDir * moveSpeed;
    }

    public void EnableWeaponCol()
    {
        equipWeaponCol.enabled = true;
    }
#endregion

    public void TakeDamage(float damageValue)
    {
        hp -= damageValue;        
    }
}
