using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IMoveable
{
    #region Event

    public static event Action OnPlayerLevelUp;

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

    public float Exp
    {
        get
        {
            return exp;
        }
        set
        {
            exp = value;

            if (!levelTable.ContainsKey(level))
            {
                return;
            }

            if (exp >= levelTable[level])
            {
                level++;

                OnPlayerLevelUp?.Invoke();

                exp = 0f;
            }
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

            if (moveDir.sqrMagnitude != 0f)
            {
                OnPlayerMove?.Invoke();
            }
            else
            {
                OnPlayerMoveStop?.Invoke();
            }
        }
    }

    public Vector3 AttackDir => (playerAttackDirObject.transform.position - transform.position).normalized;

    #endregion    

    #region Private Field

    Dictionary<int, int> levelTable = new Dictionary<int, int>();

    [SerializeField, Tooltip("체력")]
    float hp;

    [SerializeField, Tooltip("방어력")]
    float defensePower;

    [SerializeField, Tooltip("이동속도")]
    float moveSpeed;

    [SerializeField, ReadOnly]
    int level = 1;

    [SerializeField, ReadOnly]
    float exp;

    [SerializeField, ReadOnly]
    Vector3 moveDir;

    [SerializeField, ReadOnly]
    Vector3 attackDir;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        playerRigidbody = GetComponent<Rigidbody2D>();

        #endregion

        List<Dictionary<string, object>> dataList = CSVReader.Read("DataTable/PlayerLevelDataTable");

        foreach (var data in dataList)
        {
            levelTable.Add((int)data["Level"], (int)data["RequiredExp"]);
        }        
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

    public Vector3 GetMoveDir()
    {
        return (playerMoveDirObject.transform.position - transform.position).normalized;
    }
}
