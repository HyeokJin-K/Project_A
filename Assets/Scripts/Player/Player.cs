using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IMoveable
{
    #region Event
    public event Action OnPlayerMove;
    public event Action OnPlayerMoveStop;
    #endregion

    #region Public Field
    public enum PlayerAction
    {
        Idle,
        Move,
        Attack,
        Dead
    }
    public GameObject playerAttackDirObject;
    public GameObject playerMoveDirObject;
    public Rigidbody2D playerRigidbody;
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
    public Vector3 MoveDir
    {
        get
        {
            return moveDir;
        }
        set
        {
            moveDir = value;

            if (moveDir != Vector3.zero)
            {
                OnPlayerMove?.Invoke();
            }
            else
            {
                OnPlayerMoveStop?.Invoke();
            }
        }
    }
    #endregion    

    #region Private Field
    [SerializeField, Tooltip("체력")]
    float hp;

    [SerializeField, Tooltip("방어력")]
    float defensePower;

    [SerializeField, Tooltip("이동속도")]
    float moveSpeed;

    Vector3 moveDir;
    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle
    private void Awake()
    {
        #region Caching
        playerRigidbody = GetComponent<Rigidbody2D>();
        #endregion
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        MoveDir = (playerMoveDirObject.transform.position - transform.position).normalized;
        playerRigidbody.velocity = moveDir * moveSpeed;        
    }

    public void TakeDamage(float damageValue)
    {
        hp -= damageValue;
    }
}
