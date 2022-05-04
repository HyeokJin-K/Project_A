using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
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

    [SerializeField]
    int hp;
    public int Hp 
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

    [SerializeField]
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

    [SerializeField]
    float moveSpeed;

    public GameObject equipWeapon;

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

    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        //PC Àü¿ë
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");

        //playerRigidbody.velocity = new Vector2(moveX, moveY).normalized * moveSpeed;

        playerRigidbody.velocity = (playerMoveDirObject.transform.position - transform.position) * moveSpeed;
    }

    public void Attack()
    {
        
    }
    public void Damage(float receivedAttackPower)
    {

    }

   
}
