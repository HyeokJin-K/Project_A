using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerGlobalCycleTimer))]
public class Player : MonoBehaviour, IDamageable
{
    public GameObject bullet;
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

    [Tooltip("장착 무기 오브젝트")]
    public BoxCollider2D equipWeaponCol;

    public Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject go = Instantiate(bullet);
            go.transform.position = transform.position;
            go.transform.up = playerAttackDirObject.transform.up;
        }

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
        //PC 전용
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");

        //playerRigidbody.velocity = new Vector2(moveX, moveY).normalized * moveSpeed;

        playerRigidbody.velocity = (playerMoveDirObject.transform.position - transform.position) * moveSpeed;
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
