using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerGlobalCycleTimer))]
public class Player : MonoBehaviour, IDamageable
{
    public GameObject playerAttackDirObject;
    public GameObject playerMoveDirObject;

    public enum PlayerAction
    {
        Idle,
        Move,
        Attack,
        Dead
    }   

    [SerializeField, Tooltip("ü��")]
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

    [SerializeField, Tooltip("����")]
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

    [SerializeField, Tooltip("�̵��ӵ�")]
    float moveSpeed;

    [Tooltip("���� ���� ������Ʈ")]
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
