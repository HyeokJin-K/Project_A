using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MeleeWeapon
{    
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
        set
        {
            attackRange *= value;
            for(int i = 0; i < weaponCol.edgeCount; i++)
            {
                weaponCol.points[i] *= 2f;
                print(weaponCol.points[i]);
            }            
        }
    }   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            weaponCol.points[1] *= 2.5f;
            print("dd");
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Attack(collision.GetComponent<IDamageable>());     
    //}
}
