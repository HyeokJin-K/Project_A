using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField]
    protected float attackPower;
    [SerializeField]
    protected float attackDelay;
    [SerializeField]
    protected float attackRange;
    
    public float AttackPower
    {
        get
        {
            return attackPower;
        }
        set
        {
            attackPower = value;
        }
    }


}
